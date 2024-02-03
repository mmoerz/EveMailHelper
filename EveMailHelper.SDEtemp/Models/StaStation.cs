using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class StaStation
    {
        public long StationId { get; set; }
        public double? Security { get; set; }
        public double? DockingCostPerVolume { get; set; }
        public double? MaxShipVolumeDockable { get; set; }
        public int? OfficeRentalCost { get; set; }
        public int? OperationId { get; set; }
        public int? StationTypeId { get; set; }
        public int? CorporationId { get; set; }
        public int? SolarSystemId { get; set; }
        public int? ConstellationId { get; set; }
        public int? RegionId { get; set; }
        public string? StationName { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }
        public double? ReprocessingEfficiency { get; set; }
        public double? ReprocessingStationsTake { get; set; }
        public int? ReprocessingHangarFlag { get; set; }
    }
}
