using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ChatLogParser.Models
{
    public class ParserChat
    {
        public ParserChat()
        {
            Messages = new List<ParserChatMessage>();
        }

        public string ChannelName { get; set; } = null!;
        public string ChannelId { get; set; } = null!;
        public string Listener { get; set; } = null!;
        public DateTime StartedAt { get; set; }

        public ICollection<ParserChatMessage> Messages { get; set; }
    }
}
