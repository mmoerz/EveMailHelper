using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class DgmEffect
    {
        public int EffectId { get; set; }
        public string? EffectName { get; set; }
        public int? EffectCategory { get; set; }
        public int? PreExpression { get; set; }
        public int? PostExpression { get; set; }
        public string? Description { get; set; }
        public string? Guid { get; set; }
        public int? IconId { get; set; }
        public bool? IsOffensive { get; set; }
        public bool? IsAssistance { get; set; }
        public int? DurationAttributeId { get; set; }
        public int? TrackingSpeedAttributeId { get; set; }
        public int? DischargeAttributeId { get; set; }
        public int? RangeAttributeId { get; set; }
        public int? FalloffAttributeId { get; set; }
        public bool? DisallowAutoRepeat { get; set; }
        public bool? Published { get; set; }
        public string? DisplayName { get; set; }
        public bool? IsWarpSafe { get; set; }
        public bool? RangeChance { get; set; }
        public bool? ElectronicChance { get; set; }
        public bool? PropulsionChance { get; set; }
        public int? Distribution { get; set; }
        public string? SfxName { get; set; }
        public int? NpcUsageChanceAttributeId { get; set; }
        public int? NpcActivationChanceAttributeId { get; set; }
        public int? FittingUsageChanceAttributeId { get; set; }
        public string? ModifierInfo { get; set; }
    }
}
