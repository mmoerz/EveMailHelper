using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ChatLogParser.Models
{
    public class ParserChatMessage
    {
        public string Author { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime TimeStamp { get; set; }
    }
}
