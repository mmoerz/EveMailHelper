using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Security;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Models;

using EVEStandard.Models;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class BlueprintManager : IBlueprintManager
    {
        private readonly EveMailHelperContext _dbContext;
        private readonly BlueprintDbAccess _blueprintDbAccess;
        private readonly IndustryActivityDbAccess _industryActivityDbAccess;


        public BlueprintManager(
           IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _blueprintDbAccess = new(_dbContext);
            _industryActivityDbAccess = new(_dbContext);
        }

        public async Task<IndustryBlueprint?> GetBlueprint(int EveId)
        {
            return await _blueprintDbAccess.GetByIdAsync(EveId);
        }

        public async Task<TableData<IndustryBlueprint>> GetBlueprintsPaginated(
            string marketGroupFilter, string searchString, TableState state)
        {
            return await _blueprintDbAccess.GetPaginatedAsync(marketGroupFilter, searchString, state);
        }

        public async Task<List<IndustryActivity>> GetBlueprintActivity(int EveId)
        {
            return await _industryActivityDbAccess.GetByIdDeepAsync(EveId);
        }

        // Todo: filterActivity limited to a single activity type (not a list of activities)
        public async Task<ProductionPlan> GetBlueprintComponentsList(
            IndustryBlueprint blueprint,
            int filterActivity = 0
            )
        {
            _ = blueprint ?? throw new Exception("blueprint queried is null");
            if (blueprint.TypeId == 0) throw new Exception("blueprint TypeId is 0.");

            var activities = await GetBlueprintActivity(blueprint.TypeId);
            var activity = activities.Where(x => x.ActivityId == filterActivity).First();

            if (activity == null)
                throw new Exception("blueprint found without activities");

            var product = activity.Products.Single();
            ProductionPlan plan = new()
            {
                Blueprint = blueprint,
                Activity = activity,
                Product = product.ProductType,
                ProductQuantity = product.Quantity,
                //ProductQuantityFromBlueprint = product.Quantity
            };

            var resultList = await GetBlueprintComponentsForActivity(activity, filterActivity);
            foreach(var item in resultList)
            {
                plan.Add(item);
            }

            return plan;
        }

        protected async Task<List<BlueprintComponent>> GetBlueprintComponentsForActivity(
            IndustryActivity activity,
            //int productionDepth = 0,
            int filterActivityId)
            //,
            //IBlueprintComponentTree parentComponent)
        {
            _ = activity ?? throw new Exception("activity is null");

            List<BlueprintComponent> componentList = new();

            // not producing activities should be filtered
            foreach (var material in activity.Materials)
            {
                // just to have a test against null (should not happen)
                if (material.MaterialType == null)
                    throw new Exception("material.MaterialType is null");

                var component = new BlueprintComponent(null)
                {
                    EveType = material.MaterialType,
                    Name = material.MaterialType.TypeName,
                    Quantity = material.Quantity,
                    Volume = material.MaterialType?.Volume ?? 0.0,
                };
                componentList.Add(component);
                
                // now let's check if the material can be produced (has a blueprint)
                // and add it as a subcomponent
                var producedBy = await _industryActivityDbAccess.FindForProduct(
                    material.MaterialType.EveId, filterActivityId);
                if (producedBy != null)
                {
                    var result = producedBy.Products.Where(x => x.ProductTypeId == material.MaterialType.EveId).First();

                    component.QuantityFromBlueprint = result.Quantity;

                    var resultList = await GetBlueprintComponentsForActivity(producedBy, filterActivityId);
                    component.AddRange(resultList);
                }

            }
            return componentList;
        }

        public async Task<List<IndustryBlueprint>> GetBlueprintsForActivityId(int activityId)
        {
            return await _blueprintDbAccess.GetAllByActivityId(activityId);
        }
    }
}
