using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

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

        public async Task<List<EveMailSentTo>> GetReceiversFiltered(IEnumerable<string> CharacterNames, DateTime notAfterDateTime)
        {
            IQueryable<EveMailSentTo> queryTo = from sendTo in _context.EveMailSentTos
                                                select sendTo;

            IQueryable<Guid> queryChars = from ch in _context.Characters
                                          where CharacterNames.Contains(ch.Name)
                                          select ch.Id;

            

            //var List = queryChars.ToList();

            queryTo = queryTo.Where(x =>
            queryChars.Contains(x.CharacterId)
            && x.SentDate <= notAfterDateTime
            );
            var q = queryTo.ToQueryString();

            var result = await queryTo
                .AsNoTracking()
                .Include(x => x.Character)
                .Include(x => x.EveMail)
                .ToListAsync();

            return result;

        }

        public void Update(EveMail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            _context.EveMails.Update(eveMail);
        }

    }
}
