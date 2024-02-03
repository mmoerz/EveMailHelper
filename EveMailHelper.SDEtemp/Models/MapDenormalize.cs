using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class MapDenormalize
    {
        public int ItemId { get; set; }
        public int? TypeId { get; set; }
        public int? GroupId { get; set; }
        public int? SolarSystemId { get; set; }
        public int? ConstellationId { get; set; }
        public int? RegionId { get; set; }
        public int? OrbitId { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }
        public double? Radius { get; set; }
        public string? ItemName { get; set; }
        public double? Security { get; set; }
        public int? CelestialIndex { get; set; }
        public int? OrbitIndex { get; set; }
    }
}
