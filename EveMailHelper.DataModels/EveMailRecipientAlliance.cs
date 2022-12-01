
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class EveMailRecipientAlliance : EveMailRecipient
    {
        public Guid AllianceId { get; set; }
        public Alliance Alliance { get; set; } = null!;

        override public string Name
        {
            get
            {
                return Alliance != null ? Alliance.Name : "";
            }
        }
    }
}
