using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataModels.Market
{
    public class MarketPrice
    {
        public int EveTypeId { get; set; }
        public double AdjustedPrice { get; set; }
        public double AveragePrice { get; set; }

        public EveType EveType { get; set; } = null!;
    }
}
