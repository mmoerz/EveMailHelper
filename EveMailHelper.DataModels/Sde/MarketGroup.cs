using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class MarketGroup : IBaseEveId, IBaseEveObject
    {
        public MarketGroup()
        {
            InvTypes = new HashSet<EveType>();
            InverseParentGroup = new HashSet<MarketGroup>();
        }

        public int EveId { get; set; }
        public int? ParentGroupId { get; set; }
        public string? MarketGroupName { get; set; }
        public string? Description { get; set; }
        public int? IconId { get; set; }
        public bool? HasTypes { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon? Icon { get; set; }
        public virtual MarketGroup? ParentGroup { get; set; }
        public virtual ICollection<EveType> InvTypes { get; set; }
        public virtual ICollection<MarketGroup> InverseParentGroup { get; set; }
    }
}
