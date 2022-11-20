using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Interfaces
{
    public interface IBaseEveLongId
    {
        public long? EveId { get; set; }
    }
}
