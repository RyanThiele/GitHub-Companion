using GitHubCompanion.Models;
using System;

namespace GitHubCompanion
{
    /// <summary>
    /// We do not want to constantly call the API for these properties. Instead we will hold them here.
    /// </summary>
    static class Globals
    {
        internal static object LockObject = new object();
        internal static ApiGlobals Api => new ApiGlobals();

        // Application
        internal static GlobalProperty<GitHubToken> Token;
        internal static GlobalProperty<Profile> Profile;

    }

    internal class GlobalProperty<T>
    {
        public T Property { get; set; }
        public DateTime LastUpdated { get; set; }

        public GlobalProperty(T property)
        {
            Property = property;
            LastUpdated = DateTime.Now;
        }
    }

    internal class ApiGlobals : Dynamensions.Infrastructure.Base.ObservableObject
    {

        #region RequestsLeft

        private int _RequestsLeft;
        public int RequestsLeft
        {
            get { return _RequestsLeft; }
            set { _RequestsLeft = value; OnPropertyChanged(); }
        }

        #endregion RequestsLeft

        #region ResetDateTime

        private DateTime _ResetDateTime;
        public DateTime ResetDateTime
        {
            get { return _ResetDateTime; }
            set { _ResetDateTime = value; OnPropertyChanged(); }
        }

        #endregion ResetDateTime
    }
}
