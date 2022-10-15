using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dbAccess
{
    public class EveMailDbAccess
    {
        private readonly EveMailHelperContext _context;
        public EveMailDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EveMail eveMail)
        {
            await _context.EveMails.AddAsync(eveMail);
        }

        public void Update(EveMail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            _context.EveMails.Update(eveMail);
        }

    }
}
