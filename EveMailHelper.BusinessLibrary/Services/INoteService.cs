using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface INoteService
    {
        Note AddOrUpdate(Note note);
        Task<Note?> GetById(Guid id);
        Task<TableData<Note>> GetPaginated(string searchString, TableState state);
    }
}