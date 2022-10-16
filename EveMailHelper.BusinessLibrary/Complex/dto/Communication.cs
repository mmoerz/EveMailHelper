using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class Communication
    {
        public Guid Id { get; set; }
        public object obj { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public string TypeName()
        {
            return obj.GetType().Name;
        }
    }
}
