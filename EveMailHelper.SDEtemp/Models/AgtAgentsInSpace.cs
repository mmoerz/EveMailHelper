using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class AgtAgentsInSpace
    {
        public int AgentId { get; set; }
        public int? DungeonId { get; set; }
        public int? SolarSystemId { get; set; }
        public int? SpawnPointId { get; set; }
        public int? TypeId { get; set; }
    }
}
