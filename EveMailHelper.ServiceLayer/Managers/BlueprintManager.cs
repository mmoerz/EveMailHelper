using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Security;
using EveMailHelper.ServiceLayer.Interfaces;
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


        public BlueprintManager(
           IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _blueprintDbAccess = new(_dbContext);

        }

        public async Task<TableData<IndustryBlueprint>> GetBlueprintsPaginated(
            string searchString, TableState state)
        {
            return await _blueprintDbAccess.GetPaginated(searchString, state);
        }

        /*
        public async Task<TableData<Character>> GetCharactersPaginated(
            Account account, EveAccount eveAccount, string searchString, TableState state)
        {
            return await _blueprintDbAccess.GetCharactersPaginated(account, eveAccount, searchString, state);
        }
        */
    }
}
