using MudBlazor;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface INoteService
    {
        Task<Note> Update(Note note);
        Task<Note?> GetById(Guid id);
        Task Delete(Note note);
        Task<TableData<Note>> GetPaginated(string searchString, TableState state);
    }
}