using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess.Interfaces
{
    public interface IEveId<T>
    {
        public ICollection<T> GetByEveIds(ICollection<int> eveIds);
    }
}
