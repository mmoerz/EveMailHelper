using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class RamActivity
    {
        public int ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? IconNo { get; set; }
        public string? Description { get; set; }
        public bool? Published { get; set; }
    }
}
