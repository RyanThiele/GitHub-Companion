using GitHubCompanion.Models;
using System;
using System.Security;

namespace GitHubCompanion
{
    internal static class Extensions
    {
        internal static string ConvertToUnsecureString(this SecureString secureString)
        {
            if (secureString == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }

        }

        internal static SecureString ConvertToSecureString(this string s)
        {
            if (String.IsNullOrWhiteSpace(s)) return null;

            SecureString secureString = new SecureString();
            foreach (char c in s)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }

        internal static void UpdateGlobals<T>(this GitHubResponse<T> response)
        {
            if (response == null || response.Headers == null) return;

            lock (Globals.LockObject)
            {
                Globals.Api.RequestsLeft = response.Headers.RateLimitRemaining;
                Globals.Api.ResetDateTime = response.Headers.RateLimitReset;
            }
        }
    }
}
