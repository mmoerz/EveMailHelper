using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class Category : IBaseEveId, IBaseEveObject
    {
        public Category()
        {
            InvGroups = new HashSet<Group>();
        }

        public int EveId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? IconId { get; set; }
        public bool? Published { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon? Icon { get; set; }
        public virtual ICollection<Group> InvGroups { get; set; }
    }
}
