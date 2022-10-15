
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class EveMailTemplate
    {
        public void CreateMail(out EveMail eveMail)
        {
            eveMail = new EveMail
            {
                Subject = Subject,
                Content = Content
            };
        }
    }
}
