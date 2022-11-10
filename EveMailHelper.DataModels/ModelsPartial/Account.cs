using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Security
{
    public partial class Account
    {
        public void CopyShallowNoId(Account copyFrom)
        {
            NickName = copyFrom.NickName;
            FirstName = copyFrom.FirstName;
            LastName = copyFrom.LastName;
            Email = copyFrom.Email;
            Description = copyFrom.Description;
            CreatedDate = copyFrom.CreatedDate;
        }
    }
}
