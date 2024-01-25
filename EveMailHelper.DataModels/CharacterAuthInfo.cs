using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataModels
{
    public class CharacterAuthInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// quite ugly, but i haven't yet found a way to make the authentication work otherwise
        /// </summary>
        public Guid? AccountId { get; set; } = null;
        public Account? Account { get; set; } = null;
        public Guid? CharId { get; set; } = null!;
        public Character? Char { get; set; } = null;

        /// <summary>
        /// accesstoken itself
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// refreshtoken for obtaining a new accesstoken if it expired
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
        /// <summary>
        /// currently purpose / content unknown
        /// TODO: fix this
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// List of scopes that were requested for this token
        /// </summary>
        public IList<string> Scopes { get; set; } = new List<string>();

        /// <summary>
        /// expiry date of the accesstoken
        /// </summary>
        public DateTime ExpiresUTC { get; set; } = DateTime.UtcNow;
    }
}
