using System;

namespace GitHubCompanion.Models.User
{
    /// <summary>
    /// Contains common characteristics for a user.
    /// </summary>
    public class Common
    {
        /// <summary>
        /// The Id of the response.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The primary key from the database.
        /// </summary>
        public int DatabaseId { get; set; }

        /// <summary>
        /// The HTTP URL for this user
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// An URI pointing to the user's public avatar.
        /// </summary>
        public Uri AvatarUri { get; set; }

        /// <summary>
        /// A URL pointing to the user's public website/blog.
        /// </summary>
        public Uri WebsiteUrl { get; set; }

        /// <summary>
        /// The HTTP path for this user.
        /// </summary>
        public Uri ResourcePath { get; set; }

        /// <summary>
        /// The user's public profile name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user's public profile bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// The user's public profile company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The user's public profile location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The user's publicly visible profile email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Security Bug Bounty.
        /// </summary>
        public bool IsBountyHunter { get; set; }

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Campus Experts Program.
        /// </summary>
        public bool IsCampusExpert { get; set; }

        /// <summary>
        /// Whether or not this user is a GitHub Developer Program member.
        /// </summary>
        public bool IsDeveloperProgramMember { get; set; }

        /// <summary>
        /// Whether or not this user is a GitHub employee.
        /// </summary>
        public bool IsEmployee { get; set; }

        /// <summary>
        /// Whether or not the user has marked themselves as for hire.
        /// </summary>
        public bool IsHireable { get; set; }

        /// <summary>
        /// Whether or not this user is the viewing user.
        /// </summary>
        public bool IsSiteAdmin { get; set; }

        /// <summary>
        /// Whether or not this user is followed.
        /// </summary>
        public bool IsFollowing { get; set; }

        /// <summary>
        /// Whether or not the use can be followed.
        /// </summary>
        public bool IsAbleToFollow { get; set; }

        /// <summary>
        /// The date/time this model was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
