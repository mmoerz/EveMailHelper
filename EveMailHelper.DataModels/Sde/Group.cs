using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class Group : IBaseEveId, IBaseEveObject
    {
        public Group()
        {
            InvTypes = new HashSet<EveType>();
        }

        public int EveId { get; set; }
        public int? CategoryId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int? IconId { get; set; }
        public bool? UseBasePrice { get; set; }
        public bool? Anchored { get; set; }
        public bool? Anchorable { get; set; }
        public bool? FittableNonSingleton { get; set; }
        public bool? Published { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Category? Category { get; set; }
        public virtual Icon? Icon { get; set; }
        public virtual ICollection<EveType> InvTypes { get; set; }
    }
}
