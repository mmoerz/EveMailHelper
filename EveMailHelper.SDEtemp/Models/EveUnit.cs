using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class EveUnit
    {
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
    }
}
