
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels
{
    public partial class EveMailRecipient
    {
        public Guid Id { get; set; }

        virtual public string Name { get; set; } = string.Empty;
    }
}
