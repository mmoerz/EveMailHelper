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
        //public ICollection<Character> Characters { get; set; } = null!;
        public IDictionary<long, EveMailLabel> Labels = null!;
        public IDictionary<int, Character> Characters = null!;
        //public IDictionary<int, Corporation> corporations = null!;
        //public IDictionary<int, Alliance> alliances = null!;
        //public IDictionary<int, MailingList> mailingLists = null!;
        public ICollection<EVEStandard.Models.Mail> esMails = null!;
    }
}
