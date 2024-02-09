using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde.Character;

namespace EveMailHelper.DataModels.Sde.Map
{
    public partial class Constellation : IBaseEveObject
    {
        Constellation() 
        {
            SolarSystems = new HashSet<SolarSystem>();
        }

        
        public int EveId { get; set; }
        // eve id was switched with region id!!
        public int RegionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double ZMin { get; set; }
        public double ZMax { get; set; }
        public int? FactionId { get; set; }
        public double Radius { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Region Region { get; set; } = null!;
        public virtual Faction Faction { get; set; } = null!;
        public virtual ICollection<SolarSystem> SolarSystems { get; set; }
    }
}
