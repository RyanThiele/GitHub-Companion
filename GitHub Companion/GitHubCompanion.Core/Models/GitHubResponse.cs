namespace GitHubCompanion.Models
{
    /// <summary>
    /// Base class for all responses
    /// </summary>
    public class GitHubResponse<T>
    {
        public Headers.GitHubHeaders Headers { get; set; }
        public T Response { get; set; }
    }
}
