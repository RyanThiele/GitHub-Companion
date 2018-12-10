namespace GitHubCompanion.Models.Headers
{
    public enum GitHubMediaFormat
    {
        Raw,
        Text,
        Html,
        Full,
        Json
    }

    public class GitHubMediaType
    {
        public int Version { get; set; }
        public string Params { get; set; }
        public GitHubMediaFormat MediaFormat { get; set; }
    }
}
