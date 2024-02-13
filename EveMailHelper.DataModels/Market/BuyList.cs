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
    public partial class BuyList
    {
        public BuyList()
        {
            ItemList = new HashSet<BuyListItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<BuyListItem> ItemList { get; set; }
    }
}
