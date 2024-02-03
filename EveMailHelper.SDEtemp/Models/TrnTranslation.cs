using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class TrnTranslation
    {
        public int TcId { get; set; }
        public int KeyId { get; set; }
        public string LanguageId { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
