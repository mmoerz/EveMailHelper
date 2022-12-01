using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameCharacterManager
    {
        ICollection<Character> FilterNoobs(IEnumerable<Character> characters);
        Task<ICollection<Character>> LoadCharactersByName(List<string> CharacterNames);
    }
}