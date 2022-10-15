using EveMailHelper.DataAccessLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class SendTemplateToDto
    {
        //public Guid templateId;
        public EveMailTemplate Template { get; set; } = null!;
        public ICollection<Character> Characters { get; set; } = null!;
        //public ICollection<string> receiverNames = null!;
    }
}
