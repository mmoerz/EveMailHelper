using EveMailHelper.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class AddLabelDTO
    {
        public Character Character { get; set; } = null!;
        public ICollection<EVEStandard.Models.MailLabel> MailLabels { get; set; } = null!;
    }
}
