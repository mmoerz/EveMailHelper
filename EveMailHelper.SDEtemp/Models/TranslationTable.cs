using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class TranslationTable
    {
        public string SourceTable { get; set; } = null!;
        public string? DestinationTable { get; set; }
        public string TranslatedKey { get; set; } = null!;
        public int? TcGroupId { get; set; }
        public int? TcId { get; set; }
    }
}
