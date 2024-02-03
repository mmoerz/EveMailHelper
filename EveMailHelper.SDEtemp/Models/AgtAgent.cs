using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class AgtAgent
    {
        public int AgentId { get; set; }
        public int? DivisionId { get; set; }
        public int? CorporationId { get; set; }
        public int? LocationId { get; set; }
        public int? Level { get; set; }
        public int? Quality { get; set; }
        public int? AgentTypeId { get; set; }
        public bool? IsLocator { get; set; }
    }
}
