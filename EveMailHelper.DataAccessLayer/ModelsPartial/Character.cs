using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class Character
    {
        public void CopyShallow(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Age = character.Age;
            ReallifeAge = character.ReallifeAge;
            Description = character.Description;
            CreatedDate = character.CreatedDate;
            IsExcluded = character.IsExcluded;
            IsInRecruitment = character.IsInRecruitment;
            EveMailReceived.Clear();
            foreach (var received in character.EveMailReceived)
            {
                EveMailReceived.Add(received);
            }
        }
    }
}
