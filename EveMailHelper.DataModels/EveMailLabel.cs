
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class EveMailLabel
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }

        public int? EveLabelId { get; set; } = null;
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
        public int? UnreadCount { get; set; } = null;

        public Character Character { get; set; } = null!;
    }
}
