using MudBlazor;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface ICharacterService
    {
        void Delete(Character character);
        Task<Character> GetById(Guid id);
        ICollection<Character> GetByName(ICollection<string> characterNames);
        Task<TableData<Character>> GetPaginated(string searchString, TableState state);
        Task Update(Character character);
    }
}