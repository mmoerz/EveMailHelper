using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvFlag
    {
        public int FlagId { get; set; }
        public string? FlagName { get; set; }
        public string? FlagText { get; set; }
        public int? OrderId { get; set; }
    }
}
