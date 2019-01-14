using GitHubCompanion.Models;
using GitHubCompanion.Services;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace GitHubCompanion.Uwp.LocalServices
{
    class SettingsService : ISettingsService
    {
        private const string AUTHORIZATION_TOKEN_KEY = "Token";
        private const string PERSONAL_ACCESS_TOKEN_KEY = "Personal Access Token";

        #region Helpers

        private async Task<T> GetSettingAsync<T>(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return (T)(await Task.FromResult(settings.Values[key]));
        }

        private async Task<bool> SetSettingAsync<T>(string key, T value)
        {
            return await Task.FromResult(SetValue(key, value));
        }

        private async Task<bool> ClearSettingAsync(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return await Task.FromResult(settings.Values.Remove(key));
        }

        private bool SetValue<T>(string key, T value)
        {
            GitHubToken token = value as GitHubToken;
            ValidateToken(token);

            // gather data.
            string tokenKey = GetTokenKeyFromTokenType(token.TokenType);
            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            object existingValue = settings.Values[tokenKey];

            // set data.
            if (existingValue == null)
                settings.Values.Add(key, value);
            else
                settings.Values[key] = value;

            return true;
        }

        private void ValidateToken(GitHubToken token)
        {
            if (token == null) throw new ApplicationException("Token cannot be null.");
            if (!token.IsValid) throw new ApplicationException("Token is not valid and cannot be saved.");
        }

        private string GetTokenKeyFromTokenType(TokenTypes tokenType)
        {
            switch (tokenType)
            {
                case TokenTypes.AuthorizationToken:
                    return AUTHORIZATION_TOKEN_KEY;
                case TokenTypes.PersonalAccessToken:
                    return PERSONAL_ACCESS_TOKEN_KEY;
                default:
                    throw new ApplicationException("Unknown token type.");
            }
        }


        #endregion Helpers

        public Task<bool> ClearTokenAsync(TokenTypes tokenType)
        {
            return ClearSettingAsync(GetTokenKeyFromTokenType(tokenType));
        }

        public Task<GitHubToken> GetTokenAsync(TokenTypes tokenType)
        {
            return GetSettingAsync<GitHubToken>(GetTokenKeyFromTokenType(tokenType));
        }

        public Task<bool> SetTokenAsync(GitHubToken token)
        {
            return SetSettingAsync(GetTokenKeyFromTokenType(token.TokenType), token);
        }
    }
}
