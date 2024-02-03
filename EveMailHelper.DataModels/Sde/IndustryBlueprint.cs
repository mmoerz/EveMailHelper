using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryBlueprint : IBaseEveObject
    {
        public int TypeId { get; set; }
        public int? MaxProductionLimit { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual InvType Type { get; set; } = null!;
    }
}
