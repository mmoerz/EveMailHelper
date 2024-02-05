using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivity : IBaseEveObject
    {
        public int TypeId { get; set; }
        public int ActivityId { get; set; }
        public int? Time { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual EveType Type { get; set; } = null!;
    }
}
