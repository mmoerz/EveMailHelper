using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Interfaces
{
    public interface IChatService
    {
        Chat AddOrUpdate(Chat chat);
        Task<Chat?> GetById(Guid id);
        Task<TableData<Chat>> GetPaginated(string searchString, TableState state);
        Task<Chat> UpdateTracked(Chat chat);
    }
}