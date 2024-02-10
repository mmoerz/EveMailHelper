using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;
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
    public class EveTypeManager : IEveTypeManager
    {
        private readonly EveMailHelperContext _dbContext;

        private readonly EveTypeDbAccess _eveTypeDbAccess;

        public EveTypeManager(
            IDbContextFactory<EveMailHelperContext> dbContextFactory
            )
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _eveTypeDbAccess = new EveTypeDbAccess(_dbContext);
        }

        public async Task<IList<string>> SearchForEveTypeName(string regionNamePart)
        {
            var types = await _eveTypeDbAccess.SearchForName(regionNamePart);
            IList<string> result = new List<string>();
            foreach (var evetype in types)
            {
                result.Add(evetype.TypeName);
            }
            return result;
        }

        public async Task<EveType> GetByName(string eveTypeName)
        {
            return await _eveTypeDbAccess.GetByName(eveTypeName);
        }

    }
}
