using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataModels.Sde.Character
{
    public partial class NpcCorporation : IBaseEveId, IBaseEveObject
    {
        public int EveId { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Extent { get; set; } = string.Empty;
        public int? SolarSystemId { get; set; }
        public int? FriendId { get; set; }
        public int? EnemyId { get; set; }
        public int PublicShares { get; set; }
        public int InitialPrice { get; set; }
        public double MinSecurity { get; set; }
        public int? FactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? IconId { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual NpcCorporation? Friend { get; set; }
        public virtual NpcCorporation? Enemy { get; set; }

        public virtual SolarSystem SolarSystem { get; set; } = null!;
        public virtual Faction Faction { get; set; } = null!;
        public virtual Icon Icon { get; set; } = null!;
    }
}
