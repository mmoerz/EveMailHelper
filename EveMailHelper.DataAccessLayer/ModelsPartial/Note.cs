using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer.Models
{
    public partial class Note
    {
        public void CopyShallow(Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Content = note.Content;
            CreatedDate = note.CreatedDate;
            AttachedTo = note.AttachedTo;
            AttachedToId = note.AttachedToId;
        }
    }
}
