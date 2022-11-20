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
        public Guid CharacterId { get; set; } = Guid.Empty;
        public ICollection<EVEStandard.Models.MailLabel> MailLabels { get; set; } = null!;
    }
}
