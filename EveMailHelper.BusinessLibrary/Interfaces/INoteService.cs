using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface INoteService
    {
        Task<Note> Update(Note note);
        Task<Note?> GetById(Guid id);
        Task Delete(Note note);
        Task<TableData<Note>> GetPaginated(string searchString, TableState state);
    }
}