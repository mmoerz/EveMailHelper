using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class CertSkill
    {
        public int? CertId { get; set; }
        public int? SkillId { get; set; }
        public int? CertLevelInt { get; set; }
        public int? SkillLevel { get; set; }
        public string? CertLevelText { get; set; }
    }
}
