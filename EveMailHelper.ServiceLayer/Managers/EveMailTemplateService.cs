﻿using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.ServiceLibrary.Managers
{
    public class EveMailTemplateService : IEveMailTemplateService
    {
        #region injected
        #endregion
        //private readonly EveMailHelperContext dbContext = null!;
        private readonly EveMailTemplateDbAccess _dbAccess;
        private readonly RunnerWriteDb<EveMailTemplate, EveMailTemplate> _addTemplateRunner;

        public EveMailTemplateService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            var dbContext = dbContextFactory.CreateDbContext();
            _dbAccess = new(dbContext);
            _addTemplateRunner = new RunnerWriteDb<EveMailTemplate, EveMailTemplate>
                (new UpdateEveMailTemplateAction(_dbAccess), dbContext);
        }

        public EveMailTemplate AddOrUpdate(EveMailTemplate eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));

            return _addTemplateRunner.RunAction(eveMail);
        }

        public async Task<TableData<EveMailTemplate>> GetPaginated(string searchString, TableState state)
        {
            return await _dbAccess.GetPaginatedAsync(searchString, state);
        }

        public async Task<ICollection<EveMailTemplate>> GetAll()
        {
            return await _dbAccess.GetAllAsync();
        }

        public async Task<EveMailTemplate?> GetById(Guid id)
        {
            return await _dbAccess.GetByIdAsync(id);
        }
    }
}
