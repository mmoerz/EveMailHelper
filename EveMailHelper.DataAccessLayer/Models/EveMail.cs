
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class EveMail
    {
        public EveMail()
        {
            SentTo = new HashSet<EveMailSentTo>();
        }

        public Guid Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<EveMailSentTo> SentTo { get; set; }
    }
}
