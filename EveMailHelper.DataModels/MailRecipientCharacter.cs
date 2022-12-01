
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class MailRecipientCharacter : MailRecipient
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        override public string Name
        {
            get
            {
                return Character != null ? Character.Name : "";
            }
        }
    }
}
