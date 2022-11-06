
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class ChatFile
    {
        //public ChatFile()
        //{

        //}

        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public byte[] Content { get; set; } = null!;

        public Chat Chat { get; set; } = null!;
    }
}
