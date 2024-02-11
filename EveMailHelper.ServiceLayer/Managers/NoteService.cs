using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.BusinessDataAccess;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.ServiceLayer.Interfaces;

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
            return await _dbAccess.GetPaginatedAsync(searchString, state);
        }

        public async Task<Note?> GetById(Guid id)
        {
            return await _dbAccess.GetByIdAsync(id);
        }
    }
}
