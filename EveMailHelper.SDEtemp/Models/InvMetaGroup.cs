using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvMetaGroup
    {
        public int MetaGroupId { get; set; }
        public string? MetaGroupName { get; set; }
        public string? Description { get; set; }
        public int? IconId { get; set; }
    }
}
