using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Market;

using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivity : IBaseEveObject
    {
        public IndustryActivity() 
        {
            Materials = new HashSet<IndustryActivityMaterial>();
            Probabilities = new HashSet<IndustryActivityProbability>();
            Products = new HashSet<IndustryActivityProduct>();
            NormalizeProductionCosts = new HashSet<NormalizedProductionCost>();
        }
        public int TypeId { get; set; }
        public int ActivityId { get; set; }
        public int? Time { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual EveType Type { get; set; } = null!;

        public virtual ICollection<IndustryActivityMaterial> Materials { get; set; }
        public virtual ICollection<IndustryActivityProbability> Probabilities { get; set; }
        public virtual ICollection<IndustryActivityProduct> Products { get; set; }

        public virtual ICollection<NormalizedProductionCost> NormalizeProductionCosts { get; set; }
    }
}
