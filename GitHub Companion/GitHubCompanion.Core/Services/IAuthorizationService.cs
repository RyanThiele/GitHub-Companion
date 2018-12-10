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
        /// <summary>
        /// A list of scopes that this authorization is in.
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
        public string FingerPrint { get; set; }
    }

    #endregion Parameters


    public interface IAuthorizationService
    {
        Task<bool> AuthorizeWithTokenAsync(string token);
        /// <summary>
        /// Creates authorizations for a user.
        /// </summary>
        /// <param name="username">Required. The username for the authorization.</param>
        /// <param name="password">Required. And unencrypted string that contains the password.</param>
        /// <param name="TwoFactorAuthorizationCode">
        /// Optional. The Two factor authentication code. 
        /// If the two factor authentication code is unknown, leave blank, and the result to have two factor flag with the method.
        /// </param>
        /// <param name="parameters">Optional. The parameters for the authorization. Defaults to user and repos.</param>
        /// <returns></returns>
        Task<GitHubResponse<Authorization>> CreateAuthorizationAsync(string username, string password, int? TwoFactorAuthorizationCode = null, AuthorizeParameters parameters = null);

    }
}
