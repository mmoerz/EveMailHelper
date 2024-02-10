using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.ServiceLibrary.Managers;

using EveNatTools.ServiceLibrary.Utilities;

using EVEStandard;
using EVEStandard.Models.API;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public class MapManager : IMapManager
    {
        private readonly EveMailHelperContext _dbContext;

        private readonly MapDbAccess _mapDbAccess;

        public MapManager(
            IDbContextFactory<EveMailHelperContext> dbContextFactory
            )
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _mapDbAccess = new MapDbAccess(_dbContext);


        }

        public async Task<IList<string>> SearchForRegionName(string regionNamePart)
        {
            var regions = await _mapDbAccess.SearchForRegionName(regionNamePart);
            IList<string> result = new List<string>();
            foreach (var region in regions)
            {
                result.Add(region.Name);
            }
            return result;
        }


    }
}
