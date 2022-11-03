using EveMailHelper.DataAccessLayer.Migrations;
using EveMailHelper.DataAccessLayer.Models;
using System.Xml.Linq;

namespace EveMailHelper.Models
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
            character.IsExcluded = viewCharacter.IsExcluded;
            character.IsInRecruitment = viewCharacter.IsInRecruitment;
            character.CreatedDate = viewCharacter.CreatedDate != null ? 
                viewCharacter.CreatedDate.Value : DateTime.Now;
        }
    }
}
