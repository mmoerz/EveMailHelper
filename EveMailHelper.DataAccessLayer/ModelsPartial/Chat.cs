using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class Chat
    {
        public void CopyShallow(Chat chat)
        {
            Id = chat.Id;
            ChannelName = chat.ChannelName;
            Listener = chat.Listener;
            ListenerId = chat.ListenerId;
            AttachedTo = chat.AttachedTo;
            AttachedToId = chat.AttachedToId;
            SessionStarted = chat.SessionStarted;
            foreach (var message in chat.Messages)
            {
                Messages.Add(message);
            }
        }
    }
}
