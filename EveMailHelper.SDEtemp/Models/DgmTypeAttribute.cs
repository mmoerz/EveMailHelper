using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class DgmTypeAttribute
    {
        public int TypeId { get; set; }
        public int AttributeId { get; set; }
        public int? ValueInt { get; set; }
        public double? ValueFloat { get; set; }
    }
}
