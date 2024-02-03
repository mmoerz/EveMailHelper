using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityProduct
    {
        public int? TypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? Quantity { get; set; }

        public virtual IndustryActivity? IndustryActivity { get; set; }
        public virtual InvType? ProductType { get; set; }
    }
}
