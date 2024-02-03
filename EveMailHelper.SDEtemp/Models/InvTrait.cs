using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvTrait
    {
        public int TraitId { get; set; }
        public int? TypeId { get; set; }
        public int? SkillId { get; set; }
        public double? Bonus { get; set; }
        public string? BonusText { get; set; }
        public int? UnitId { get; set; }
    }
}
