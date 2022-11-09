using MudBlazor;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface ICharacterService
    {
        void Delete(Character character);
        Character GetCharactersById(Guid id);
        ICollection<Character> GetCharactersByName(ICollection<string> characterNames);
        Task<TableData<Character>> GetPaginated(string searchString, TableState state);
        Task Update(Character character);
    }
}