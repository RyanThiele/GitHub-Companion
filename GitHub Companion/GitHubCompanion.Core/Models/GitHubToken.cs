using System;

namespace GitHubCompanion.Models
{
    /// <summary>
    /// The GitHub access token types.
    /// </summary>
    public enum TokenTypes
    {
        /// <summary>
        /// A token that this application creates. It contains a default access.
        /// </summary>
        AuthorizationToken,

        /// <summary>
        /// A personal access token is created by the user in GitHub under Profile => Security.
        /// </summary>
        PersonalAccessToken
    }

    /// <summary>
    /// A container to hold the token used for accessing information form the API.
    /// </summary>
    public class GitHubToken
    {
        /// <summary>
        /// A String holding the token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// A <see cref="TokenTypes"/> containing the type of token.
        /// </summary>
        public TokenTypes TokenType { get; }

        /// <summary>
        /// Returns true if token is valid; Otherwise false.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !String.IsNullOrEmpty(Token);
            }
        }

        /// <summary>
        /// Creates a new <see cref="GitHubToken"/> model.
        /// </summary>
        /// <param name="token">A string representing a token.</param>
        /// <param name="tokenType">The <see cref="TokenTypes"/> containing the type of token.</param>
        public GitHubToken(string token, TokenTypes tokenType)
        {
            Token = token;
            TokenType = tokenType;
        }
    }
}
