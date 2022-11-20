using EveMailHelper.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class AddMailDTO
    {
        public IDictionary<long, MailLabel> Labels = null!;
        public IDictionary<int, Character> Characters = null!;
        public IDictionary<int, Corporation> Corporations = null!;
        public IDictionary<int, Alliance> Alliances = null!;
        public IDictionary<int, MailList> MailLists = null!;
        public ICollection<EVEStandard.Models.Mail> esMails = null!;
    }
}
