
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class EveMailSentTo
    {
        public Guid Id { get; set; }
        public Guid EveMailId { get; set; }
        public Guid CharacterId { get; set; }
        public DateTime SentDate { get; set; }

        public virtual EveMail EveMail { get; set; } = null!;
        public virtual Character Character { get; set; } = null!;

    }
}
