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

namespace EveMailHelper.ServiceLibrary.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private EveMailHelperContext _dbContext;
        private NewAccessTokenDbAccess _tokenDbAccess;
        private EVEStandardAPI _esiClient;
        private readonly SSOv2 _sso;

        public AuthenticationManager(
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            //EveMailHelperContext eveNatDBContext,
            //NewAccessTokenDbAccess tokenDbAccess,
            EVEStandardAPI esiClient,
            SSOv2 sSOv2)
        {
            _dbContext = dbContextFactory.CreateDbContext();
                        
            _tokenDbAccess = new(_dbContext);
            _esiClient = esiClient;
            _sso = sSOv2;
        }

        public string GetEveAuthorizationUrl(List<string>? scopes = null)
        {
            var newAuthInfo = _tokenDbAccess.RegisterNewCharAuthInfo(scopes);
            _dbContext.SaveChanges();

            //var authorization = _esiClient.SSO.AuthorizeToEVEUri(scopes, );

            var authorization = _sso.AuthorizeToSSOBasicAuthUri(newAuthInfo.Id.ToString(), scopes);

            return authorization;
        }

        public async Task FinalizeEveAuthentication(string code, string state)
        {
            Guid.TryParse(state, out Guid stateGuid);

            var accessToken = await _sso.VerifyAuthorizationForBasicAuthAsync(code);
            var character = await _sso.GetCharacterDetailsAsync(accessToken.AccessToken);

            var authInfo = _tokenDbAccess.FindCharacterAuthInfoById(stateGuid);

            authInfo.AccessToken = accessToken.AccessToken;
            authInfo.RefreshToken = accessToken.RefreshToken;
            authInfo.ExpiresUTC = accessToken.ExpiresUtc;

            // if the corresponding char is not yet in the database, create it
            if (!_dbContext.Characters.
                    Where(c => c.EveId == character.CharacterId).
                    Any())
            {
                _dbContext.Characters.Add(new Character()
                {
                    EveId = character.CharacterId,
                    Name = character.CharacterName,
                });
                _dbContext.SaveChanges();
            }
            var dbCharacter = _dbContext.Characters.
                Where(c => c.EveId == character.CharacterId).
                Single();
            if (dbCharacter.Name != character.CharacterName)
                throw new Exception("charactername mismatch");

            authInfo.Char = dbCharacter;
            _dbContext.SaveChanges();
        }

        public bool IsValidCharacterAuthInfoId(Guid guid)
        {
            return _dbContext.CharacterAuthInfos
                .Where(c => c.Id == guid)
                .FirstOrDefault() != null;
        }

        public bool HasAuthInfo(Character character)
        {
            return GetLastAuthInfoForCharacter(character) != null;
        }

        public CharacterAuthInfo? GetLastAuthInfoForCharacter(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            return _dbContext.CharacterAuthInfos
                .Where(ca => ca.Char != null && ca.Char.Id == character.Id)
                .OrderByDescending(ca => ca.ExpiresUTC)
                .FirstOrDefault();
        }

        public async Task RefreshTokenAsync(Character character)
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

        public bool AuthTokenNeedsRefresh(Character character)
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