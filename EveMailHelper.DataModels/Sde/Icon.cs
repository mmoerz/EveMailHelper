using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class Icon : IBaseEveId, IBaseEveObject
    {
        public Icon()
        {
            ChrRaces = new HashSet<CharacterRace>();
            InvCategories = new HashSet<Category>();
            InvGroups = new HashSet<Group>();
            InvMarketGroups = new HashSet<MarketGroup>();
            InvTypes = new HashSet<EveType>();
        }

        public int EveId { get; set; }
        public string? IconFile { get; set; }
        public string? Description { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual ICollection<CharacterRace> ChrRaces { get; set; }
        public virtual ICollection<Category> InvCategories { get; set; }
        public virtual ICollection<Group> InvGroups { get; set; }
        public virtual ICollection<MarketGroup> InvMarketGroups { get; set; }
        public virtual ICollection<EveType> InvTypes { get; set; }
    }
}
