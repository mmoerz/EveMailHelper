using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataModels;
using EveMailHelper.DataAccessLayer.Context;
using EveNatTools.ServiceLibrary.Utilities;

namespace EveNatTools.BusinessLogicLibrary
{
    public class NewAccessTokenDbAccess
    {
        private readonly EveMailHelperContext _context;

        public NewAccessTokenDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        //public CharacterAuthInfo GetNew()
        //{
        //    return 
        //}

        /// <summary>
        /// returns all authentication infos for a character
        /// since there might be more than one and they will 
        /// differ by type of scope.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public IList<CharacterAuthInfo> FindCharacterAuthInfoByChar(
            Character character,
            IList<string>? neededScopes = null)
        {
            return _context.CharacterAuthInfos
                .Where(cai => cai.CharId == character.Id
                        //&& cai.ExpiresUTC > DateTime.UtcNow
                //c => c.Char != null
                        //&& c.Char.Id == character.Id
                        && (neededScopes == null
                            || cai.Scopes.ContainsAllItems(neededScopes))
                       )
                .OrderByDescending(cai => cai.ExpiresUTC)
                .Include(c => c.Char)
                .ToList();
        }

        public CharacterAuthInfo FindCharacterAuthInfoById(Guid guid)
        {
            return _context.CharacterAuthInfos.Single(c => c.Id == guid);
        }

        public void Add(CharacterAuthInfo characterAuthInfo)
        {
            _context.Add(characterAuthInfo);
        }

        public void Update()
        {

        }

        public CharacterAuthInfo RegisterNewCharAuthInfo(List<string>? scopes = null)
        {
            var authInfo = new CharacterAuthInfo()
            {
                Scopes = scopes ?? new List<string>(),
            };
            _context.CharacterAuthInfos.Add(authInfo);

            return authInfo;
        }

    }
}