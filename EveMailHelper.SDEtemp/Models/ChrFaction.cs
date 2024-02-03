using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class ChrFaction
    {
        public int FactionId { get; set; }
        public string? FactionName { get; set; }
        public string? Description { get; set; }
        public int? RaceIds { get; set; }
        public int? SolarSystemId { get; set; }
        public int? CorporationId { get; set; }
        public double? SizeFactor { get; set; }
        public int? StationCount { get; set; }
        public int? StationSystemCount { get; set; }
        public int? MilitiaCorporationId { get; set; }
        public int? IconId { get; set; }
    }
}
