using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Security
{
    public class Account
    {
        public Account()
        {
            Characters = new HashSet<Character>();
            EveAccounts= new HashSet<EveAccount>();
        }

        public Guid Id { get; set; }
        public string NickName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<Character> Characters { get; set; }
        public ICollection<EveAccount> EveAccounts { get; set; }
    }
}
