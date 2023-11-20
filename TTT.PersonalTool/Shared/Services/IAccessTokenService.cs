namespace TTT.PersonalTool.Shared.Services
{
    public interface IAccessTokenService
    {
        /// <summary>
        /// Get token from local storage
        /// </summary>
        /// <param name="tokenName">name token | ex: jwt_token</param>
        /// <returns>Return string token</returns>
        Task<string> GetAccessTokenAsync(string tokenName);

        /// <summary>
        /// Change token in local storage
        /// </summary>
        /// <param name="tokenName">name token | ex: jwt_token</param>
        /// <param name="tokenValue">string token</param>
        /// <returns>Return Task State</returns>
        Task SetAccessTokenAsync(string tokenName, string tokenValue);

        /// <summary>
        /// Remove token in local storage
        /// </summary>
        /// <param name="tokenName">name token | ex: jwt_token</param>
        /// <returns>Return Task State</returns>
        Task RemoveAccessTokenAsync(string tokenName);
    }
}
