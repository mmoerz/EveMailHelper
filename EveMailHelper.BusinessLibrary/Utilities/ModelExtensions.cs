using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public static class ModelExtensions
    {
        public static Character CopyShallow(this Character character, EVEStandard.Models.CharacterInfo info)
        {
            character.Name = info.Name;
            character.Description = info.Description;
            character.Birthday = info.Birthday;
            character.SecurityStatus= info.SecurityStatus;
            character.Title= info.Title;
            
            return character; 
        }
    }
    
}
