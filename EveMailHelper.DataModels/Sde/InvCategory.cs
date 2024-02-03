using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class InvCategory : IBaseEveId, IBaseEveObject
    {
        public InvCategory()
        {
            InvGroups = new HashSet<InvGroup>();
        }

        public int EveId { get; set; }
        public string? CategoryName { get; set; }
        public int? IconId { get; set; }
        public bool? Published { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon? Icon { get; set; }
        public virtual ICollection<InvGroup> InvGroups { get; set; }
    }
}
