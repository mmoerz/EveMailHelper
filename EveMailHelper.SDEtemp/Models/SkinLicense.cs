using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class SkinLicense
    {
        public int LicenseTypeId { get; set; }
        public int? Duration { get; set; }
        public int? SkinId { get; set; }
    }
}
