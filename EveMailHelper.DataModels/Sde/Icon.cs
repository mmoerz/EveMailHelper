using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class Icon : IBaseEveId, IBaseEveObject
    {
        public Icon()
        {
            ChrRaces = new HashSet<ChrRace>();
            InvCategories = new HashSet<InvCategory>();
            InvGroups = new HashSet<InvGroup>();
            InvMarketGroups = new HashSet<InvMarketGroup>();
            InvTypes = new HashSet<InvType>();
        }

        public int EveId { get; set; }
        public string? IconFile { get; set; }
        public string? Description { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual ICollection<ChrRace> ChrRaces { get; set; }
        public virtual ICollection<InvCategory> InvCategories { get; set; }
        public virtual ICollection<InvGroup> InvGroups { get; set; }
        public virtual ICollection<InvMarketGroup> InvMarketGroups { get; set; }
        public virtual ICollection<InvType> InvTypes { get; set; }
    }
}
