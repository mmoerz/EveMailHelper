using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class WarCombatZone
    {
        public int CombatZoneId { get; set; }
        public string? CombatZoneName { get; set; }
        public int? FactionId { get; set; }
        public int? CenterSystemId { get; set; }
        public string? Description { get; set; }
    }
}
