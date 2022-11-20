using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels
{
    public class MailList : IBaseEveId
    {
        public Guid Id { get; set; }
        public int EveId { get; set; }
        public string Name { get; set; } = null!;
    }
}
