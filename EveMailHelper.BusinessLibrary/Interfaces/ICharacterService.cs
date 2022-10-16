using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface ICharacterService
    {
        void Delete(Character character);
        ICollection<Character> GetCharactersByName(ICollection<string> characterNames);
        Task<TableData<Character>> GetPaginated(string searchString, TableState state);
        Task Update(Character character);
    }
}