using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IAuthenticationManager
    {
        bool AuthTokenNeedsRefresh(Character character);
        Task FinalizeEveAuthentication(string code, string state);
        string GetEveAuthorizationUrl(List<string>? scopes = null);

        /// <summary>
        /// get the latest accesstoken for a character from the database
        /// </summary>
        /// <param name="character">to fetch accesstoken for</param>
        /// <returns></returns>
        CharacterAuthInfo? GetLastAuthInfoForCharacter(Character character);

        /// <summary>
        /// checks that there really is an accesstoken to refresh for the character
        /// </summary>
        /// <param name="character">to check for an accesstoken</param>
        /// <returns></returns>
        bool HasAuthInfo(Character character);
        bool IsValidCharacterAuthInfoId(Guid guid);

        /// <summary>
        /// refreshes the accesstoken of an character
        /// TODO: check if is really expired already (if not do nothing)
        /// </summary>
        /// <param name="character"></param>
        Task RefreshTokenAsync(Character character);
    }
}