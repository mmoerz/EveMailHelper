using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dbAccess;
using EveMailHelper.BusinessLibrary.Interfaces;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class ChatService : IChatService
    {
        #region injected
        #endregion
        private readonly ChatDbAccess _dbAccess;
        //private readonly RunnerWriteDb<Note, Note> _addTemplateRunner;

        public ChatService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            var dbContext = dbContextFactory.CreateDbContext();
            _dbAccess = new(dbContext);
        }

        public Chat AddOrUpdate(Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            _dbAccess.Update(chat);

            return chat;
        }

        public async Task<TableData<Chat>> GetPaginated(string searchString, TableState state)
        {
            return await _dbAccess.GetPaginated(searchString, state);
        }

        public async Task<Chat?> GetById(Guid id)
        {
            return await _dbAccess.GetById(id);
        }
    }
}
