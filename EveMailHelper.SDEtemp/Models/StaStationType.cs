using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class StaStationType
    {
        public int StationTypeId { get; set; }
        public double? DockEntryX { get; set; }
        public double? DockEntryY { get; set; }
        public double? DockEntryZ { get; set; }
        public double? DockOrientationX { get; set; }
        public double? DockOrientationY { get; set; }
        public double? DockOrientationZ { get; set; }
        public int? OperationId { get; set; }
        public int? OfficeSlots { get; set; }
        public double? ReprocessingEfficiency { get; set; }
        public bool? Conquerable { get; set; }
    }
}
