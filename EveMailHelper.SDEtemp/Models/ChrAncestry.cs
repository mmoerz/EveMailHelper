using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class ChrAncestry
    {
        public int AncestryId { get; set; }
        public string? AncestryName { get; set; }
        public int? BloodlineId { get; set; }
        public string? Description { get; set; }
        public int? Perception { get; set; }
        public int? Willpower { get; set; }
        public int? Charisma { get; set; }
        public int? Memory { get; set; }
        public int? Intelligence { get; set; }
        public int? IconId { get; set; }
        public string? ShortDescription { get; set; }
    }
}
