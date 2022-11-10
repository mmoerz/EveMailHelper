using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Security
{
    public class EveAccount
    {
        public EveAccount()
        {
            Characters = new HashSet<Character>();
        }

        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = null!;

        public Account Account { get; set; } = null!;
        public ICollection<Character> Characters { get; set; }
    }
}
