using EveMailHelper.DataAccessLayer.Models;

using System.Security.Permissions;

namespace EveMailHelper.Models
{
    public class ViewCharacter
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; } = null!;
        public int? ReallifeAge { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool IsExcluded { get; set; } = false;
        public bool IsInRecruitment { get; set; } = false;
        public DateTime? CreatedDate { get; set; }

        
        public void CopyShallow(Character character)
        {
            Name = character.Name;
            Age = character.Age;
            ReallifeAge = character.ReallifeAge;
            Description = character.Description;
            IsExcluded = character.IsExcluded;
            IsInRecruitment = character.IsInRecruitment;
            CreatedDate = character.CreatedDate;
        }

        public void CopyShallow(ViewCharacter character)
        {
            Name = character.Name;
            Age = character.Age;
            ReallifeAge = character.ReallifeAge;
            Description = character.Description;
            IsExcluded = character.IsExcluded;
            IsInRecruitment = character.IsInRecruitment;
            CreatedDate = character.CreatedDate;
        }

    }
}
