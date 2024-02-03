using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class StaOperation
    {
        public int? ActivityId { get; set; }
        public int OperationId { get; set; }
        public string? OperationName { get; set; }
        public string? Description { get; set; }
        public int? Fringe { get; set; }
        public int? Corridor { get; set; }
        public int? Hub { get; set; }
        public int? Border { get; set; }
        public int? Ratio { get; set; }
        public int? CaldariStationTypeId { get; set; }
        public int? MinmatarStationTypeId { get; set; }
        public int? AmarrStationTypeId { get; set; }
        public int? GallenteStationTypeId { get; set; }
        public int? JoveStationTypeId { get; set; }
    }
}
