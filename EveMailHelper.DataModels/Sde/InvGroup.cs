using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class InvGroup : IBaseEveId, IBaseEveObject
    {
        public InvGroup()
        {
            InvTypes = new HashSet<InvType>();
        }

        public int EveId { get; set; }
        public int? CategoryId { get; set; }
        public string? GroupName { get; set; }
        public int? IconId { get; set; }
        public bool? UseBasePrice { get; set; }
        public bool? Anchored { get; set; }
        public bool? Anchorable { get; set; }
        public bool? FittableNonSingleton { get; set; }
        public bool? Published { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual InvCategory? Category { get; set; }
        public virtual Icon? Icon { get; set; }
        public virtual ICollection<InvType> InvTypes { get; set; }
    }
}
