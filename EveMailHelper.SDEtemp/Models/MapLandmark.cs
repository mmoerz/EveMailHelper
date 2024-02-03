using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class MapLandmark
    {
        public int LandmarkId { get; set; }
        public string? LandmarkName { get; set; }
        public string? Description { get; set; }
        public int? LocationId { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }
        public int? IconId { get; set; }
    }
}
