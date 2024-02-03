using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityMaterial
    {
        public int? TypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? Quantity { get; set; }

        public virtual IndustryActivity? IndustryActivity { get; set; }
        public virtual InvType? MaterialType { get; set; }
        public virtual InvType? Type { get; set; }
    }
}
