using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class MapSolarSystemJump
    {
        public int? FromRegionId { get; set; }
        public int? FromConstellationId { get; set; }
        public int FromSolarSystemId { get; set; }
        public int ToSolarSystemId { get; set; }
        public int? ToConstellationId { get; set; }
        public int? ToRegionId { get; set; }
    }
}
