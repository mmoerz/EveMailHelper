using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class RamInstallationTypeContent
    {
        public int InstallationTypeId { get; set; }
        public int AssemblyLineTypeId { get; set; }
        public int? Quantity { get; set; }
    }
}
