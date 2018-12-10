using System;
using System.Linq;
using System.Net.Http.Headers;

namespace GitHubCompanion.Models.Headers
{
    public class GitHubHeaders
    {
        /// <summary>
        /// The Id of the request.
        /// </summary>
        public string GitHubRequestId { get; }

        /// <summary>
        /// The media type.
        /// </summary>
        public GitHubMediaType GitHubMediaType { get; }
        public int RateLimit { get; }
        public int RateLimitRemaining { get; }
        public DateTime RateLimitReset { get; }
        public string XFrameOptions { get; }
        public GitHubMediaType MediaType { get; }
        public GitHubOptionHeader GitHubOptionHeader { get; }

        public GitHubHeaders(HttpResponseHeaders keyValues)
        {
            if (keyValues == null) return;
            if (keyValues.Contains("X-RateLimit-Limit"))
            {
                int rateLimit;
                if (Int32.TryParse(keyValues.GetValues("X-RateLimit-Limit").FirstOrDefault(), out rateLimit)) RateLimit = rateLimit;
            }

            if (keyValues.Contains("X-RateLimit-Remaining"))
            {
                int rateLimitRemaining;
                if (Int32.TryParse(keyValues.GetValues("X-RateLimit-Remaining").FirstOrDefault(), out rateLimitRemaining)) RateLimitRemaining = rateLimitRemaining;
            }

            if (keyValues.Contains("X-RateLimit-Reset"))
            {
                int rateReset;
                if (Int32.TryParse(keyValues.GetValues("X-RateLimit-Reset").FirstOrDefault(), out rateReset))
                {
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    RateLimitReset = epoch.AddSeconds(rateReset);
                }
            }

            if (keyValues.Contains("X-GitHub-OTP"))
            {
                GitHubOptionHeader = new GitHubOptionHeader(keyValues.GetValues("X-GitHub-OTP").FirstOrDefault());
            }


            if (keyValues.Contains("X-GitHub-Media-Type"))
            {
                string mediaValue = keyValues.GetValues("X-GitHub-Media-Type").FirstOrDefault();
                string[] mediaValues = mediaValue?.Split(';');
                GitHubMediaType = new GitHubMediaType();

                if (mediaValues != null || mediaValues.Length > 0)
                {
                    string value = GetHeaderValueAfterString(mediaValues, "github.v");
                    if (value != null)
                    {
                        int version;
                        if (Int32.TryParse(value, out version)) GitHubMediaType.Version = version;
                    }


                    GitHubMediaType.Params = GetHeaderValueAfterString(mediaValues, "params=");
                    value = GetHeaderValueAfterString(mediaValues, "format=");
                    if (value != null)
                    {
                        if (value.ToLower() == "raw")
                            GitHubMediaType.MediaFormat = GitHubMediaFormat.Raw;
                        else if (value.ToLower() == "text")
                            GitHubMediaType.MediaFormat = GitHubMediaFormat.Text;
                        else if (value.ToLower() == "html")
                            GitHubMediaType.MediaFormat = GitHubMediaFormat.Html;
                        else if (value.ToLower() == "full")
                            GitHubMediaType.MediaFormat = GitHubMediaFormat.Full;
                        else if (value.ToLower() == "json")
                            GitHubMediaType.MediaFormat = GitHubMediaFormat.Json;
                    }
                }



            }
        }

        private string GetHeaderValueAfterString(string[] headers, string stringToTruncate)
        {
            return headers.Where(x => x.Trim().StartsWith(stringToTruncate)).Select(x => x.Trim().Substring(stringToTruncate.Length)).FirstOrDefault();
        }

    }
}
