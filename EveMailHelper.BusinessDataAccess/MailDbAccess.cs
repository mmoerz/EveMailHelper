using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using System.Security.Cryptography;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace EveMailHelper.BusinessDataAccess
{
    public class MailDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MailDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Mail eveMail)
        {
            await _context.Mails.AddAsync(eveMail);
        }

        public async Task<Mail> GetByEveId(long eveId)
        {
            return await _context.Mails
                .Where(x => x.EveId == eveId)
                .FirstAsync();
        }

        public async Task<long> GetMaxEveMailIdAsync(Guid characterId)
        {
            var result = await _context.Mails
                .Where(m => m.FromId == characterId)
                .MaxAsync(m => m.EveId);
            return result ?? 0;
        }

        private class IdName
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;

            public bool Equals(IdName obj)
            {
                return Id == obj.Id;
            }

            public override bool Equals(object? obj)
            {
                if (obj.GetType() == typeof(IdName))
                    return Equals((IdName)obj);
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
        }

        public async Task<List<string>> GetReceiversFiltered(IEnumerable<string> CharacterNames, DateTime notAfterDateTime)
        {
            var notAfterParam = new SqlParameter("notAfterDateTime", notAfterDateTime);

            // raw sql approach (can't use non entity types here -> EF Core 7.0 necessary)
            var queryAllowed = _context.Characters.FromSqlRaw(
                @"SELECT Distinct c.*
                  FROM [Character] c
                  JOIN evemailrecipient er ON(er.CharacterId = c.id)
                  JOIN eve.mail m on(m.id = er.MailId)
                  WHERE m.CreatedDate <= @notAfterDateTime
                  ", notAfterParam
                  );

            var queryDisallowed = _context.Characters.FromSqlRaw(
                @"SELECT Distinct c.*
                  FROM [Character] c
                  JOIN evemailrecipient er ON(er.CharacterId = c.id)
                  JOIN eve.mail m on(m.id = er.MailId)
                  WHERE m.CreatedDate > @notAfterDateTime
                  ", notAfterParam
                  );

            var filteredAllowed = queryAllowed
                .Where(x => CharacterNames.Contains(x.Name));
            var filteredDisallowed = queryDisallowed
                .Where(x => CharacterNames.Contains(x.Name));
            var finalAllowed = filteredAllowed.Except(filteredDisallowed).Select(x => x.Name);

            //// first part only gets characters that have an email sent to before the datetime
            //var queryChars = from ch in _context.Characters
            //                 where CharacterNames.Contains(ch.Name)
            //                 && !ch.IsExcluded
            //                 && !ch.IsInRecruitment
            //                 && ch.Status == CharacterStatus.None
            //                 select ch.Id;
            ////var help = queryChars.ToDictionary(x => x.Id);

            //var queryReceived = from recChar in _context.MailRecipientCharacters
            //                    where queryChars.Contains(recChar.CharacterId)
            //                    select recChar.MailId;
            //var MailIds = await queryReceived.ToListAsync();

            ////var queryReceivers = _context.MailRecipients.FromSqlRaw("SELECT * from dbo.MailRecipients WHERE CharacterId in ()")
            //var queryAllowed = from mail in _context.Mails
            //                  //where queryReceived.Contains(mail.Id)
            //              where MailIds.Contains(mail.Id)
            //              && mail.CreatedDate <= notAfterDateTime
            //              select mail;
            //var queryDisallowed = from mail in _context.Mails
            //                          //where queryReceived.Contains(mail.Id)
            //                      where MailIds.Contains(mail.Id)
            //                      && mail.CreatedDate > notAfterDateTime
            //                      select mail;
            //var MailIdsAllowed = await queryAllowed
            //    .Include(x => x.Recipients)
            //    .ToListAsync();
            //var MailIdsDisallowed = await queryDisallowed
            //    .Include(x => x.Recipients)
            //    .ToListAsync();

            //MailIdsAllowed.Where(x => x.Recipients.Contains())
            

            //var queryTo = from sendTo in _context.EveMailSentTos
            //                  //join character in _context.Characters on sendTo.CharacterId equals character.Id 
            //              where queryChars.Contains(sendTo.CharacterId)
            //              //group new { SentDate = sendTo.SentDate, Name = character.Name } by sendTo.CharacterId into grp
            //              group new { SentDate = sendTo.SentDate } by sendTo.CharacterId into grp
            //              where grp.Max(x => x.SentDate) <= notAfterDateTime
            //              //&& sendTo.SentDate <= notAfterDateTime
            //              //orderby sendTo.Character.Name
            //              //group sendTo.CharacterId
            //              select new Guid(grp.Key.ToString());

            //var interim = await queryTo
            //    .ToListAsync();

            ////.Select((key) => key.SentDate )
            ////queryTo.Distinct();

            //var queryNames = from character in _context.Characters
            //                 where interim.Contains(character.Id)
            //                 select character.Name;
            var firstPart = await finalAllowed
                .AsNoTracking()
                //.OrderBy(x => x)
                //.Include(x => x.EveMailReceived)
                //.Include(x => x.)
                .ToListAsync();

            // second part returns characters not already registered as characters
            var queryCharsInDb = from character in _context.Characters
                                 where CharacterNames.Contains(character.Name)
                                 select character.Name;
            //List<string> newCharNames = new();
            var newCharNames = CharacterNames.ToArray();
            newCharNames.Except(queryCharsInDb.ToArray());
            
            //foreach (var Name in CharacterNames)
            //    if (!queryCharsInDb.Contains(Name))
            //        newCharNames.Add(Name);

            var result = firstPart.Concat(newCharNames);

            return result
                .OrderBy(x => x)
                .ToList();
        }

        public void Update(Mail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            _context.Mails.Update(eveMail);
        }

    }
}
