﻿using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityProduct : IBaseEveObject
    {
        public int? TypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? Quantity { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual IndustryActivity? IndustryActivity { get; set; }
        public virtual InvType? ProductType { get; set; }
    }
}
