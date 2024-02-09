using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Sde.Character;

namespace EveMailHelper.DataModels.Sde.Map
{
    public partial class Region
    {
        Region() 
        {
            Constellations = new HashSet<Constellation>();
            SolarSystems = new HashSet<SolarSystem>();
        }

        public int EveId { get; set; }
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
        public int Nebula { get; set; }
        public double? Radius { get; set; }

        public virtual Faction? Faction { get; set; }

        public virtual ICollection<Constellation> Constellations { get; set; }
        public virtual ICollection<SolarSystem> SolarSystems { get; set; }

    }
}
