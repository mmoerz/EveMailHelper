using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class Character
    {
        public Character()
        {
            EveMailReceived = new HashSet<EveMailSentTo>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool IsExcluded { get; set; } = false;
        public bool IsInRecruitment { get; set; } = false;
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<EveMailSentTo> EveMailReceived { get; set; }
    }
}
