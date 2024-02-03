using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class RamAssemblyLineTypeDetailPerGroup
    {
        public int AssemblyLineTypeId { get; set; }
        public int GroupId { get; set; }
        public double? TimeMultiplier { get; set; }
        public double? MaterialMultiplier { get; set; }
        public double? CostMultiplier { get; set; }
    }
}
