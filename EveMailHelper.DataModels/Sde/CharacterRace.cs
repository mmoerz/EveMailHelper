using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class CharacterRace : IBaseEveId, IBaseEveObject
    {
        public CharacterRace()
        {
            InvTypes = new HashSet<EveType>();
        }

        public int EveId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public int? IconId { get; set; }
        public string? ShortDescription { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual Icon? Icon { get; set; }
        public virtual ICollection<EveType> InvTypes { get; set; }
        
    }
}
