using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvItem
    {
        public int ItemId { get; set; }
        public int TypeId { get; set; }
        public int OwnerId { get; set; }
        public int LocationId { get; set; }
        public int FlagId { get; set; }
        public int Quantity { get; set; }
    }
}
