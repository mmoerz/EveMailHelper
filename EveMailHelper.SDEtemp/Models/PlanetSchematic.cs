using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class PlanetSchematic
    {
        public int SchematicId { get; set; }
        public string? SchematicName { get; set; }
        public int? CycleTime { get; set; }
    }
}
