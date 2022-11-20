
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class EveMailRecipientCorporation : EveMailRecipient
    {
        public Guid CorporationId { get; set; }
        public Corporation Corporation { get; set; } = null!;

        override public string Name
        {
            get
            {
                return Corporation.Name;
            }
        }
    }
}
