using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public class CharacterStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
