using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public Guid AttachedToId { get; set; } 
        public Character AttachedTo { get; set; } = null!;
    }
}
