using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid AuthorId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Created { get; set; }
        public Chat Chat { get; set; } = null!;
        public Character Author { get; set; } = null!;
    }
}
