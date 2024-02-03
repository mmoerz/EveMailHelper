using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class TrnTranslationColumn
    {
        public int? TcGroupId { get; set; }
        public int TcId { get; set; }
        public string TableName { get; set; } = null!;
        public string ColumnName { get; set; } = null!;
        public string? MasterId { get; set; }
    }
}
