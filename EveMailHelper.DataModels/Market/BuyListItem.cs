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
    public class BuyListItem
    {
        public Guid Id { get; set; }
        public Guid BuyListId { get; set; }
        
        public int EveTypeId { get; set; }
        public double Quantity { get; set; }

        public virtual BuyList BuyList { get; set; } = null!;
        public virtual EveType EveType { get; set; } = null!;
    }
}
