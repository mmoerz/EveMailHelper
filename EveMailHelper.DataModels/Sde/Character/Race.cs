using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde.Map;

using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde.Character
{
    public partial class Race : IBaseEveId, IBaseEveObject
    {
        public Race()
        {
            InvTypes = new HashSet<EveType>();
            Factions = new HashSet<Faction>();
            
        }

        public int EveId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? IconId { get; set; }
        public string? ShortDescription { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon? Icon { get; set; }
        public virtual ICollection<EveType> InvTypes { get; set; }
        public virtual ICollection<Faction> Factions { get; set; }
        

    }
}
