using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataModels.Market
{
    /// <summary>
    /// contains the estimated costs for running an activity for a blueprint
    /// </summary>
    public class NormalizeProductionCost : IBaseUpdateableEveObject
    {
        public int EveTypeId { get; set; }
        public int ActivityId { get; set; }
        public int NumberOfRuns { get; set; }
        public double DirectJobCost { get; set; }
        public double DirectComponentCost {get; set; }
        public double BestPriceJobCost { get; set; }
        public double BestPriceComponentCost { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPricePerUnit {  get; set; }

        public DateTime LastUpdatedFromEve { get; set; } = DateTime.UtcNow;
        public virtual EveType EveType { get; set; } = null!;
        public virtual IndustryActivity IndustryActivity { get; set; } = null!;
        public virtual EveType Product { get; set; } = null!;
        
    }
}
