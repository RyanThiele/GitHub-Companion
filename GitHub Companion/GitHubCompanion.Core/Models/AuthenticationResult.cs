namespace GitHubCompanion.Models
{
    /// <summary>
    /// When requesting authentication with the API this class will be returned form the service.
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// Read only. True if authentication successful; Otherwise false.
        /// </summary>
        public bool AuthenticationSuccessful { get; set; }

        /// <summary>
        /// Read Only. Null if there is no information; Otherwise, it will contain information for two-factor authentication (2fa)
        /// </summary>
        public Headers.GitHubOptionHeader OptionHeader { get; set; }
    }
}
