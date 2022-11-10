using EveMailHelper.BusinessLibrary;
using EveMailHelper.DataAccessLayer;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.ModelExtensions;

using EVEStandard;
using EVEStandard.Models.SSO;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.ServiceLayer.Interfaces;
using EveNatTools.BusinessLogicLibrary;
using Microsoft.EntityFrameworkCore;
using EveMailHelper.BusinessDataAccess;
using System.Security.Claims;
using EveMailHelper.DataModels.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using EVEStandard.API;

namespace EveMailHelper.ServiceLibrary.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly EveMailHelperContext _dbContext;
        private readonly NewAccessTokenDbAccess _tokenDbAccess;
        private readonly CharacterDbAccess _characterDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private readonly SSOv2 _sso;

        public AuthenticationManager(
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            //EveMailHelperContext eveNatDBContext,
            EVEStandardAPI esiClient,
            SSOv2 sSOv2)
        {
            _dbContext = dbContextFactory.CreateDbContext();
                        
            _tokenDbAccess = new(_dbContext);
            _characterDbAccess = new(_dbContext);
            _esiClient = esiClient;
            _sso = sSOv2;
        }

        public string GetEveAuthorizationUrl(List<string>? scopes = null)
        {
            var newAuthInfo = _tokenDbAccess.RegisterNewCharAuthInfo(scopes);
            _dbContext.SaveChanges();

            var authorization = _sso.AuthorizeToSSOBasicAuthUri(newAuthInfo.Id.ToString(), scopes);

            return authorization;
        }

        /// <summary>
        /// use the Guid in code to find the right authentication information stored in 
        /// the db, attach it to the (new) character, account and eveaccount and save
        /// everything to the db.
        /// </summary>
        /// <param name="code">Guid as a string</param>
        /// <param name="state"></param>
        /// <returns>the character stored in the db</returns>
        /// <exception cref="Exception">thrown on validation errors</exception>
        public async Task<DataModels.Character> FinalizeEveAuthentication(ClaimsPrincipal principal, string code, string state)
        {
            if (!Guid.TryParse(state, out Guid stateGuid))
                throw new Exception("state must contain a valid Guid");

            var accessToken = await _sso.VerifyAuthorizationForBasicAuthAsync(code);
            var character = await _sso.GetCharacterDetailsAsync(accessToken.AccessToken);

            var dbAuthInfo = _tokenDbAccess.FindCharacterAuthInfoById(stateGuid);

            dbAuthInfo.AccessToken = accessToken.AccessToken;
            dbAuthInfo.RefreshToken = accessToken.RefreshToken;
            dbAuthInfo.ExpiresUTC = accessToken.ExpiresUtc;

            // if the corresponding char is not yet in the database, create it
            DataModels.Character dbCharacter = null!;
            if (!_dbContext.Characters.
                    Where(c => c.EveId == character.CharacterId).
                    Any())
            {
                dbCharacter = new DataModels.Character()
                {
                    EveId = character.CharacterId,
                    Name = character.CharacterName,
                };
                //_dbContext.SaveChanges();
            }
            else {
                dbCharacter = _dbContext.Characters.
                    Where(c => c.EveId == character.CharacterId).
                    Single();
            }
            // store already used Account / EveAccount into the char
            if (IsAlreadyAuthenticated(principal))
            {
                var account = GetAccountFromPrincipal(principal);
                var eveaccount = GetEveAccountFromPrincipal(principal);
                dbCharacter.Account = account;
                dbCharacter.EveAccount = eveaccount;
            } 

            // verify that no mismatches have happened
            if (dbCharacter.Name != character.CharacterName)
                throw new Exception("charactername mismatch");

            // save changes to database
            _characterDbAccess.Update(dbCharacter);
            dbAuthInfo.Char = dbCharacter;
            _dbContext.Update(dbAuthInfo);
            _dbContext.SaveChanges();

            return dbCharacter;
        }

        public const string CLAIM_ACCOUNT_ID = "at.natoka.evemailhelpers.account.id";
        public const string CLAIM_EVEACCOUNT_ID = "at.natoka.evemailhelpers.eveaccount.id";

        public static Guid GetCharacterGuidFromPrincipal(ClaimsPrincipal principal)
        {
            var result = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (result == null || !Guid.TryParse(result.Value, out Guid resultGuid))
                throw new Exception("claims identifier not a valid Guid");
            return resultGuid;
        }

        public static bool IsAlreadyAuthenticated(ClaimsPrincipal principal)
        {
            try
            {
                GetCharacterGuidFromPrincipal(principal);
            }
            catch { return false; }
            return true;
        }

        public Account GetAccountFromPrincipal(ClaimsPrincipal principal)
        {
            var accountId = principal.FindFirst(CLAIM_ACCOUNT_ID);
            if (accountId == null)
                throw new Exception("no User already authenticated");
            var parsed = Guid.TryParse(accountId.ToString(), out Guid accountGuid);
            if (!parsed)
                throw new Exception("account id not a valid Guid");
            return _dbContext.Accounts
                .Where(x => x.Id == accountGuid)
                .Single();
        }

        public EveAccount GetEveAccountFromPrincipal(ClaimsPrincipal principal)
        {
            var eveaccountId = principal.FindFirst(CLAIM_EVEACCOUNT_ID);
            if (eveaccountId == null)
                throw new Exception("no User already authenticated");
            var parsed = Guid.TryParse(eveaccountId.ToString(), out Guid accountGuid);
            if (!parsed)
                throw new Exception("eveaccount id not a valid Guid");
            return _dbContext.EveAccounts
                .Where(x => x.Id == accountGuid)
                .Single();
        }

        public static ClaimsIdentity GetClaimsPrincipal(ClaimsPrincipal principal, DataModels.Character character)
        {
            //// is the principal already 
            //if (principal != null && IsAlreadyAuthenticated(principal))
            //{
            //    var currentGuid = GetCharacterGuidFromPrincipal(principal);
            //    // then return the claimidentity containing all the evemailhelper claims
            //    if (currentGuid == character.Id)
            //        return principal.Identities
            //            .Where(x => x.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Any())
            //            .Where(x => x.Claims.Where(c => c.Type == ClaimTypes.Name).Any())
            //            .Where(x => x.Claims.Where(c => c.Type == CLAIM_ACCOUNT_ID).Any())
            //            .Where(x => x.Claims.Where(c => c.Type == CLAIM_EVEACCOUNT_ID).Any())
            //            .Single();
            //}
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, character.Id.ToString()),
                new Claim(ClaimTypes.Name, character.Name),
                // should be stored in the db for the character
                new Claim(CLAIM_ACCOUNT_ID, character.Account.Id.ToString()),
                new Claim(CLAIM_EVEACCOUNT_ID, character.EveAccount.Id.ToString())
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsValidCharacterAuthInfoId(Guid guid)
        {
            return _dbContext.CharacterAuthInfos
                .Where(c => c.Id == guid)
                .FirstOrDefault() != null;
        }

        public bool HasAuthInfo(DataModels.Character character)
        {
            return GetLastAuthInfoForCharacter(character) != null;
        }

        public CharacterAuthInfo? GetLastAuthInfoForCharacter(DataModels.Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            return _dbContext.CharacterAuthInfos
                .Where(ca => ca.Char != null && ca.Char.Id == character.Id)
                .OrderByDescending(ca => ca.ExpiresUTC)
                .FirstOrDefault();
        }

        public async Task RefreshTokenAsync(DataModels.Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            var authInfo = GetLastAuthInfoForCharacter(character);
            if (authInfo == null)
                throw new InvalidOperationException($"Cannot request new token when no refresh token can be found for the character {character.Name}");
            var accessTokenDetails = await _sso.GetNewBasicAuthAccessAndRefreshTokenAsync(authInfo.RefreshToken);
            
            UpdateAccessToken(authInfo, accessTokenDetails);
        }

        protected void UpdateAccessToken(CharacterAuthInfo characterAuthInfo, AccessTokenDetails accessTokenDetails)
        {
            _= characterAuthInfo ?? throw new ArgumentNullException(nameof(characterAuthInfo));
            _= accessTokenDetails ?? throw new ArgumentNullException(nameof(accessTokenDetails));

            characterAuthInfo.ShallowCopyFrom(accessTokenDetails);
            _dbContext.SaveChanges();
        }

        public bool AuthTokenNeedsRefresh(DataModels.Character character)
        {
            var lastAuth = GetLastAuthInfoForCharacter(character);
            if (lastAuth != null && (lastAuth.ExpiresUTC.ToLocalTime() - DateTime.Now).TotalMinutes > 0)
            {
                return false;
            }
            return true;
        }

    }
}