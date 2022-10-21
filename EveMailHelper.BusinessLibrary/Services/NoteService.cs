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
    public class NoteService : INoteService
    {
        #region injected
        #endregion
        private readonly NoteDbAccess _dbAccess;
        private readonly EveMailHelperContext _context;
        //private readonly RunnerWriteDb<Note, Note> _addTemplateRunner;

        public NoteService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
            _dbAccess = new(_context);

        }

        public async Task Delete(Note note)
        {
            _dbAccess.Delete(note);
            await _context.SaveChangesAsync();
        }

        public async Task<Note> Update(Note note)
        {
            _ = note ?? throw new ArgumentNullException(nameof(note));

            _dbAccess.Update(note);
            await _context.SaveChangesAsync();

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
