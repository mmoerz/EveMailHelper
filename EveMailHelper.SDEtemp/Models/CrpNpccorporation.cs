using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class CrpNpccorporation
    {
        public int CorporationId { get; set; }
        public string? Size { get; set; }
        public string? Extent { get; set; }
        public int? SolarSystemId { get; set; }
        public int? InvestorId1 { get; set; }
        public int? InvestorShares1 { get; set; }
        public int? InvestorId2 { get; set; }
        public int? InvestorShares2 { get; set; }
        public int? InvestorId3 { get; set; }
        public int? InvestorShares3 { get; set; }
        public int? InvestorId4 { get; set; }
        public int? InvestorShares4 { get; set; }
        public int? FriendId { get; set; }
        public int? EnemyId { get; set; }
        public int? PublicShares { get; set; }
        public int? InitialPrice { get; set; }
        public double? MinSecurity { get; set; }
        public bool? Scattered { get; set; }
        public int? Fringe { get; set; }
        public int? Corridor { get; set; }
        public int? Hub { get; set; }
        public int? Border { get; set; }
        public int? FactionId { get; set; }
        public double? SizeFactor { get; set; }
        public int? StationCount { get; set; }
        public int? StationSystemCount { get; set; }
        public string? Description { get; set; }
        public int? IconId { get; set; }
    }
}
