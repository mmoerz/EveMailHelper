using EveMailHelper.DataModels;

namespace EveMailHelper.Web.Models
{
    public static class CharacterExtension
    {
        public static void CopyShallow(this Character character, ViewCharacter viewCharacter)
        {
            _ = viewCharacter ?? throw new ArgumentNullException(nameof(viewCharacter));
            character.Name = viewCharacter.Name;
            character.Age = viewCharacter.Age;
            character.ReallifeAge = viewCharacter.ReallifeAge;
            character.Description = viewCharacter.Description;
            character.Status = character.Status;
            character.IsExcluded = viewCharacter.IsExcluded;
            character.IsInRecruitment = viewCharacter.IsInRecruitment;
            character.CreatedDate = viewCharacter.CreatedDate != null ? 
                viewCharacter.CreatedDate.Value : DateTime.Now;
        }
    }
}
