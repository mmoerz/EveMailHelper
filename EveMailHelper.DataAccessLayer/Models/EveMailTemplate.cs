
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class EveMailTemplate
    {
        public EveMailTemplate()
        {
            EveMailsGenerated = new HashSet<EveMail>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<EveMail> EveMailsGenerated { get; set; }
    }
}
