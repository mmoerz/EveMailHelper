using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dbAccess;
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
    public class NoteService : INoteService
    {
        #region injected
        #endregion
        private readonly NoteDbAccess _dbAccess;
        //private readonly RunnerWriteDb<Note, Note> _addTemplateRunner;

        public NoteService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            var dbContext = dbContextFactory.CreateDbContext();
            _dbAccess = new(dbContext);

        }

        public Note AddOrUpdate(Note note)
        {
            _ = note ?? throw new ArgumentNullException(nameof(note));

            _dbAccess.Update(note);

            return note;
        }

        public async Task<TableData<Note>> GetPaginated(string searchString, TableState state)
        {
            return await _dbAccess.GetPaginated(searchString, state);
        }

        public async Task<Note?> GetById(Guid id)
        {
            return await _dbAccess.GetById(id);
        }
    }
}
