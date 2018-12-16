using System;
using System.ComponentModel;

namespace GitHubCompanion.Models.Headers
{
    [DefaultValue(GitHubOptionHeaderType.Unknown)]
    public enum GitHubOptionHeaderType
    {
        Unknown,
        SMS,
        AuthenticatorApp
    }

    public class GitHubOptionHeader
    {
        public bool IsRequired { get; set; }
        public GitHubOptionHeaderType Type { get; set; }

        public GitHubOptionHeader()
        {

        }

        public GitHubOptionHeader(string headerValue)
        {
            if (String.IsNullOrWhiteSpace(headerValue)) return;

            string[] values = headerValue.Split(';');
            if (values.Length < 2) return;

            // all requirements have been met. disect the values.
            IsRequired = values[0].ToLower() == "required";

            if (values[1].ToLower().Trim() == "sms")
                Type = GitHubOptionHeaderType.SMS;
            else if (values[1].ToLower().Trim() == "app")
                Type = GitHubOptionHeaderType.AuthenticatorApp;
        }
    }
}
