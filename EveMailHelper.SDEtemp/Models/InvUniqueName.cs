using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvUniqueName
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int? GroupId { get; set; }
    }
}
