using System;

namespace GitHubCompanion.Services.Version4.Responses
{
    public class ViewerResponse
    {
        public ViewerReponseData Data { get; set; }
    }

    public class ViewerReponseData
    {
        public Viewer Viewer { get; set; }
    }

    public class Viewer
    {
        public string Id { get; set; }
        public int DatabaseId { get; set; }
        public Uri Url { get; set; }
        public Uri AvatarUrl { get; set; }
        public Uri WebsiteUrl { get; set; }
        public Uri ResourcePath { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool IsBountyHunter { get; set; }
        public bool IsCampusExpert { get; set; }
        public bool IsDeveloperProgramMember { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsHireable { get; set; }
        public bool IsSiteAdmin { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAbleToFollow { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
