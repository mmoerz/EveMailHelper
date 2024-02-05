using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class EveType : IBaseEveId, IBaseEveObject
    {
        public EveType()
        {
            IndustryActivities = new HashSet<IndustryActivity>();
        }

        public int EveId { get; set; }
        public int? GroupId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Mass { get; set; }
        public double? Volume { get; set; }
        public double? Capacity { get; set; }
        public int? PortionSize { get; set; }
        public int? RaceId { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? Published { get; set; }
        public int? MarketGroupId { get; set; }
        public int? IconId { get; set; }
        public int? SoundId { get; set; }
        public int? GraphicId { get; set; }

        public virtual Group? Group { get; set; }
        public virtual Icon? Icon { get; set; }
        public virtual MarketGroup? MarketGroup { get; set; }
        public virtual ChrRace? Race { get; set; }
        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;
        public virtual IndustryBlueprint? IndustryBlueprint { get; set; }
        public virtual ICollection<IndustryActivity> IndustryActivities { get; set; }
        
    }
}
