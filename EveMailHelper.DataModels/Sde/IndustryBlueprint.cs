using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryBlueprint 
    {
        public int TypeId { get; set; }
        public int? MaxProductionLimit { get; set; }

        public virtual InvType Type { get; set; } = null!;
    }
}
