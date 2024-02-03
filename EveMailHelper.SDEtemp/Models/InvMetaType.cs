using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvMetaType
    {
        public int TypeId { get; set; }
        public int? ParentTypeId { get; set; }
        public int? MetaGroupId { get; set; }
    }
}
