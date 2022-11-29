using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels
{
    public class Corporation : IBaseEveId
    {
        public Corporation()
        {
            Members = new HashSet<Character>();
        }

        public Guid Id { get; set; }
        public int EveId { get; set; }

        public string Name { get; set; } = null!;
        public Guid? AllianceId { get; set; }
        public Guid? CeoId { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? DateFounded { get; set; } = null;
        public string Description { get; set; } = null!;
        public Guid? FactionId { get; set; } = null;
        public Guid? HomeStationId { get; set; }
        public int MemberCount { get; set; }
        public long? Shares { get; set; } = null;
        public float TaxRate { get; set; }
        public string Ticker { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool WarEligible { get; set; }
        public bool EveDeletedInGame { get; set; } = false;
        public DateTime EveLastUpdated { get; set; } = DateTime.UtcNow;

        public virtual Alliance? Alliance { get; set; } = null!;
        public virtual Character? Ceo { get; set; } = null!;
        public virtual Character? Creator { get; set; } = null!;
        public virtual ICollection<Character> Members { get; set; }
    }
}
