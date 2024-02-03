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

        public virtual InvType Type { get; set; } = null!;
    }
}
