using EVEStandard.Models.SSO;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.ModelExtensions
{
    public static class CharacterAuthInfoExtension
    {
        /// <summary>
        /// Maps (copies) all the properties of an AccessTokenDetails object to 
        /// </summary>
        /// <param name="characterAuthInfo"></param>
        /// <param name="accessTokenDetails"></param>
        public static void ShallowCopyFrom(this CharacterAuthInfo characterAuthInfo, AccessTokenDetails accessTokenDetails)
        {
            characterAuthInfo.AccessToken = accessTokenDetails.AccessToken;
            characterAuthInfo.RefreshToken = accessTokenDetails.RefreshToken;
            characterAuthInfo.ExpiresUTC = accessTokenDetails.ExpiresUtc;
            characterAuthInfo.TokenType = accessTokenDetails.TokenType;
        }
    }
}
