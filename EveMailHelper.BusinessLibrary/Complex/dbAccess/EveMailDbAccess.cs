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

        public async Task<List<string>> GetReceiversFiltered(IEnumerable<string> CharacterNames, DateTime notAfterDateTime)
        {
            // first part only gets characters that have an email sent to before the datetime
            IQueryable<Guid> queryChars = from ch in _context.Characters
                                          where CharacterNames.Contains(ch.Name)
                                          select ch.Id;

            var queryTo = from sendTo in _context.EveMailSentTos
                          //join character in _context.Characters on sendTo.CharacterId equals character.Id 
                          where queryChars.Contains(sendTo.CharacterId)
                          //group new { SentDate = sendTo.SentDate, Name = character.Name } by sendTo.CharacterId into grp
                          group new { SentDate = sendTo.SentDate } by sendTo.CharacterId into grp
                          where grp.Max(x => x.SentDate) <= notAfterDateTime
                                  //&& sendTo.SentDate <= notAfterDateTime
                          //orderby sendTo.Character.Name
                          //group sendTo.CharacterId
                          select new Guid(grp.Key.ToString());

            var interim = await queryTo
                .ToListAsync();

            //.Select((key) => key.SentDate )
            //queryTo.Distinct();

            var queryNames = from character in _context.Characters
                                               where interim.Contains(character.Id)
                                               select character.Name;
            var firstPart = await queryNames
                .AsNoTracking()
                //.OrderBy(x => x)
                //.Include(x => x.EveMailReceived)
                //.Include(x => x.)
                .ToListAsync();

            // second part returns characters not already registered as characters
            var queryCharsInDb = from character in _context.Characters
                                 where CharacterNames.Contains(character.Name)
                                 select character.Name;
            List<string> newCharNames = new();
            foreach (var Name in CharacterNames)
                if (!queryCharsInDb.Contains(Name))
                    newCharNames.Add(Name);

            //var List = queryChars.ToList();

            //queryTo = queryTo.Where(x =>
            //queryChars.Contains(x.CharacterId)
            //&& x.SentDate <= notAfterDateTime
            //);
            //var q = queryTo.ToQueryString();

            var result = firstPart.Concat(newCharNames);

            return result
                .OrderBy(x => x)
                .ToList();
        }

        public void Update(EveMail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            _context.EveMails.Update(eveMail);
        }

    }
}
