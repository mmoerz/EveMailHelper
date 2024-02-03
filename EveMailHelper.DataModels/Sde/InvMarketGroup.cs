using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class InvMarketGroup : IBaseEveId, IBaseEveObject
    {
        public InvMarketGroup()
        {
            InvTypes = new HashSet<InvType>();
            InverseParentGroup = new HashSet<InvMarketGroup>();
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
        public virtual InvMarketGroup? ParentGroup { get; set; }
        public virtual ICollection<InvType> InvTypes { get; set; }
        public virtual ICollection<InvMarketGroup> InverseParentGroup { get; set; }
    }
}
