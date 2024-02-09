using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataModels.Sde.Character
{
    public partial class Faction : IBaseEveId, IBaseEveObject
    {
        Faction()
        {
            Regions = new HashSet<Region>();
            Constellations = new HashSet<Constellation>();
            SolarSystems = new HashSet<SolarSystem>();
            NpcCorporations = new HashSet<NpcCorporation>();
        }

        public int EveId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RaceId { get; set; }
        public int SolarSystemId { get; set; }
        public int? CorporationId { get; set; }
        public double SizeFactor { get; set; }
        public int? MilitiaCorporationId { get; set; }
        public int IconId { get; set; }
        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon Icon { get; set; } = null!;
        public virtual Race? Race { get; set; } = null!;
        public virtual SolarSystem SolarSystem { get; set; } = null!;
        public virtual NpcCorporation? Corporation { get; set; }
        public virtual NpcCorporation? MilitiaCorporation { get; set; } = null!;
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Constellation> Constellations { get; set; }
        public virtual ICollection<SolarSystem> SolarSystems { get; set; }
        public virtual ICollection<NpcCorporation> NpcCorporations { get; set; }
    }
}
