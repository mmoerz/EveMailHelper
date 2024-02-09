using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde.Character;

namespace EveMailHelper.DataModels.Sde.Map
{
    public partial class SolarSystem : IBaseEveId, IBaseEveObject
    {
        public int EveId { get; set; }

        public int RegionId { get; set; }
        public int ConstellationId { get; set; }
        // here was id befoe
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
        public double Luminosity { get; set; }
        public bool Border { get; set; }
        public bool Fringe { get; set; }
        public bool Corridor { get; set; }
        public bool Hub { get; set; }
        public bool International { get; set; }
        public bool Regional { get; set; }
        public double Security { get; set; }
        public int? FactionId { get; set; }
        public double Radius { get; set; }
        public int? SunTypeId { get; set; }
        public string? SecurityClass { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Region Region { get; set; } = null!;
        public virtual Constellation Constellation { get; set; } = null!;

        public virtual Faction? Faction { get; set; }

    }
}
