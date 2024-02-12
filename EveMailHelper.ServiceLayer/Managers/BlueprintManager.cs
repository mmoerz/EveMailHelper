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

        public async Task<TableData<IndustryBlueprint>> GetBlueprintsPaginated(
            string groupFilter, string searchString, TableState state)
        {
            return await _blueprintDbAccess.GetPaginatedAsync(groupFilter, searchString, state);
        }

        public async Task<List<IndustryActivity>> GetBlueprintActivity(int EveId)
        {
            return await _industryActivityDbAccess.GetByIdDeepAsync(EveId);
        }

        public async Task<BlueprintComponentTree> GetBlueprintComponentsList(
            IndustryBlueprint blueprint,
            int filterActivity = 0
            )
        {
            _ = blueprint ?? throw new Exception("blueprint queried is null");
            if (blueprint.TypeId == 0) throw new Exception("blueprint TypeId is 0.");

            var activities = await GetBlueprintActivity(blueprint.TypeId);
            var activity = activities.Where(x => x.ActivityId == 11).First();

            if (activity == null)
                throw new Exception("Oh weh blueprint ohne Activity");

            var first = activity.Products.Single();
            BlueprintComponentTree main = new()
            {
                EveId = first.ProductTypeId, // would be the blueprint eveTypeId --> activity.Type.EveId,
                Name = first.ProductType.TypeName,
                Quantity = 0,
                QuantityFromBlueprint = first.Quantity,
                Volume = first.ProductType.Volume ?? 0.0,
                //SubComponents = components,
            };

            var components = await GetBlueprintComponentsForActivity(activity, filterActivity, main);

            return main;
        }

        protected async Task<BlueprintComponentTree> GetBlueprintComponentsForActivity(
            IndustryActivity activity,
            //int productionDepth = 0,
            int filterActivityId,
            BlueprintComponentTree parentComponent)
        {
            _ = activity ?? throw new Exception("activity is null");

            // not producing activities should be filtered
            foreach (var material in activity.Materials)
            {
                // just to have a test against null (should not happen)
                if (material.MaterialType == null)
                    throw new Exception("material.MaterialType is null");

                var component = new BlueprintComponentTree()
                {
                    EveId = material.MaterialType.EveId,
                    //ProductionDepth = productionDepth + 1,
                    Name = material.MaterialType.TypeName,
                    Quantity = material.Quantity,
                    Volume = material.MaterialType?.Volume ?? 0.0,
                };
                
                // now let's check if the material can be produced (has a blueprint)
                // and add it as a subcomponent
                var producedBy = await _industryActivityDbAccess.FindForProduct(
                    material.MaterialType.EveId, filterActivityId);
                if (producedBy.Any())
                {
                    var first = producedBy.First();
                    var result = first.Products.Where(x => x.ProductTypeId == material.MaterialType.EveId).First();

                    component.QuantityFromBlueprint = result.Quantity;

                    //component.SubComponents = await GetBlueprintComponentsForActivity(
                    //    first, 
                    //    productionDepth + 1, 
                    //    filterActivityId);
                    var call = await GetBlueprintComponentsForActivity(
                        first,
                        filterActivityId,
                        component);
                }

                parentComponent.Add(component);
            }

            return parentComponent;
        }

    }
}
