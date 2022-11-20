
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class EveMailRecipientMailList : EveMailRecipient
    {
        public Guid MailListId { get; set; }
        public MailList MailList { get; set; } = null!;

        override public string Name
        {
            get
            {
                return MailList.Name;
            }
        }
    }
}
