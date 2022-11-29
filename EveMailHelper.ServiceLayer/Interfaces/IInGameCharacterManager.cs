using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameCharacterManager
    {
        Task<ICollection<Character>> LoadCharactersByName(List<string> CharacterNames);
    }
}