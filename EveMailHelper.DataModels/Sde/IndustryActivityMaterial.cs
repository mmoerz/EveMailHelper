﻿using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityMaterial : IBaseEveObject
    {
        public int TypeId { get; set; }
        public int ActivityId { get; set; }
        public int MaterialTypeId { get; set; }
        public int Quantity { get; set; } = 0;

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual IndustryActivity IndustryActivity { get; set; } = null!;
        public virtual EveType MaterialType { get; set; } = null!;
        public virtual EveType Type { get; set; } = null!;
    }
}
