
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class MailRecipient
    {
        public Guid Id { get; set; }

        public Guid MailId { get; set; }

        virtual public string Name { get; set; } = string.Empty;

        public Mail Mail { get; set; } = null!;

        public override string ToString()
        {
            return Name;
        }
    }
}
