using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess.Interfaces
{
    public interface IUpdateModel<T>
    {
        T Update(T entity);
    }
}
