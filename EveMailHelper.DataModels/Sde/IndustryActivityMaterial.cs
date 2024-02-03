using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityMaterial : IBaseEveObject
    {
        public int? TypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? Quantity { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual IndustryActivity? IndustryActivity { get; set; }
        public virtual InvType? MaterialType { get; set; }
        public virtual InvType? Type { get; set; }
    }
}
