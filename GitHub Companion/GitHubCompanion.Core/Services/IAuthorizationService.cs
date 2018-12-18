using GitHubCompanion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{

    #region Parameters

    public class AuthorizeParameters
    {
        public AuthorizeParameters()
        {
            Scopes = new string[] { "user", "repos" };
        }

        /// <summary>
        /// A list of scopes that this authorization is in. Defaults to user and repos.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }

        /// <summary>
        /// Required. A note to remind you what the OAuth token is for. 
        /// Tokens not associated with a specific OAuth application (i.e. personal access tokens) must have a unique note.
        /// </summary>
        [Required]
        public string Note { get; set; }

        /// <summary>
        /// A URL to remind you what app the OAuth token is for.
        /// </summary>
        public Uri Note_Url { get; set; }

        /// <summary>
        /// The 20 character OAuth app client key for which to create the token.
        /// </summary>
        [StringLength(20)]
        public string Client_Id { get; set; }

        /// <summary>
        /// The 40 character OAuth app client secret for which to create the token.
        /// </summary>
        [StringLength(40)]
        public string Client_Secret { get; set; }

        /// <summary>
        /// A unique string to distinguish an authorization from others created for the same client ID and user.
        /// </summary>
        public string Fingerprint { get; set; }
    }

    #endregion Parameters


    public interface IAuthorizationService
    {
        /// <summary>
        /// Authenticate with the API.
        /// </summary>
        /// <param name="username">Required. The username for the authorization.</param>
        /// <param name="password">Required. And unencrypted string that contains the password.</param>
        /// <param name="tfaCode">
        /// Optional. The Two factor authentication code. 
        /// If the two factor authentication code is unknown, leave blank, and the result to have two factor flag with the method.
        /// </param>
        /// <param name="cancellationToken">The cancellation token of the call.</param>
        /// <returns>The result of the authentication request.</returns>
        Task<AuthenticationResult> AuthenticateAsync(string username, string password, string tfaCode = null);

        /// <summary>
        /// Authenticates with a token.
        /// </summary>
        /// <param name="token">Required. A string representing a token.</param>
        /// <returns>True if authentication is successful; Otherwise, false.</returns>
        Task<AuthenticationResult> AuthenticateWithTokenAsync(string token);

        /// <summary>
        /// Authenticates with a token.
        /// </summary>
        /// <param name="token">Required. A string representing a token.</param>
        /// <returns>True if authentication is successful; Otherwise, false.</returns>
        Task<AuthenticationResult> AuthenticateWithPersonalAccessTokenAsync(string personalAccessToken);


        /// <summary>
        /// Creates authorizations for a user.
        /// </summary>
        /// <param name="username">Required. The username for the authorization.</param>
        /// <param name="password">Required. And unencrypted string that contains the password.</param>
        /// <param name="TwoFactorAuthorizationCode">
        /// Optional. The Two factor authentication code. 
        /// If the two factor authentication code is unknown, leave blank, and the result to have two factor flag with the method.
        /// </param>
        /// <param name="parameters">Required. The parameters for the authorization.</param>
        /// <returns></returns>
        Task<GitHubResponse<Authorization>> CreateAuthorizationTokenForAppAsync(AuthorizeParameters parameters, string username, string password, string TwoFactorAuthorizationCode = null);

    }
}
