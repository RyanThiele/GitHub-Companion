using GitHubCompanion.Models;
using GitHubCompanion.Models.User;
using GitHubCompanion.Services.Version4;
using GitHubCompanion.Services.Version4.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class UserServiceV4 : GitHubServiceV4Base, IUserService
    {

        private async Task<GitHubResponse<T>> QueryAsync<T>(string query)
        {
            GitHubResponse<T> result = new GitHubResponse<T>();

            using (HttpClient client = CreateHttpClient("2e895067dd3aa04fb26253852580a387b09b5404"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                using (StringReader reader = new StringReader(query))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = reader.ReadLine();
                        stringBuilder.Append(line.Trim());
                    }
                };

                query = stringBuilder.ToString();

                HttpResponseMessage response = await client.PostAsync(API_ENDPOINT, new StringContent(query));
                result.Headers = new Models.Headers.GitHubHeaders(response.Headers);
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.Response = await response.Content.ReadAsAsync<T>();
                }

                return result;
            }
        }

        public async Task<GitHubResponse<Uri>> GetAvatarUrlAsync()
        {
            GitHubResponse<Uri> result = new GitHubResponse<Uri>();
            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(@"
{
   ""query"": ""query { viewer { avatarUrl } }"",
   ""variables"" : null

}");
            result.Response = response.Response.Data.Viewer.AvatarUrl;
            result.Headers = response.Headers;
            return result;
        }

        public async Task<GitHubResponse<string>> GetCompanyAsync()
        {
            GitHubResponse<string> result = new GitHubResponse<string>();
            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(@"
{
   ""query"": ""query { viewer { company } }"",
   ""variables"" : null

}");
            result.Response = response.Response.Data.Viewer.Company;
            result.Headers = response.Headers;
            return result;
        }

        //        public async Task<GitHubResponse<string>> GetCompanyHtmlAsync()
        //        {
        //            GitHubResponse<string> result = new GitHubResponse<string>();
        //            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(@"
        //{
        //   ""query"": ""query { viewer { companyHTML } }"",
        //   ""variables"" : null

        //}");
        //            result.Response = response.Response.Data.Viewer.CompanyHtml;
        //            result.Headers = response.Headers;
        //            return result;
        //        }

        public async Task<GitHubResponse<string>> GetUserBioAsync()
        {
            GitHubResponse<string> result = new GitHubResponse<string>();
            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(@"
{
   ""query"": ""query { viewer { bio } }"",
   ""variables"" : null

}");
            result.Response = response.Response.Data.Viewer.Bio;
            result.Headers = response.Headers;
            return result;
        }

        //        public async Task<GitHubResponse<string>> GetUserBioHtmlAsync()
        //        {
        //            GitHubResponse<string> result = new GitHubResponse<string>();
        //            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(@"
        //{
        //   ""query"": ""query { viewer { bioHTML } }"",
        //   ""variables"" : null

        //}");
        //            result.Response = response.Response.Data.Viewer.BioHtml;
        //            result.Headers = response.Headers;
        //            return result;
        //        }

        public async Task<GitHubResponse<Common>> GetCommonAsync()
        {

            string query = @"
{
   ""query"": ""query { 
    viewer { 
    id, 
    databaseId, 
    url, 
    avatarUrl, 
    websiteUrl, 
    resourcePath,
    name,
    login,
    bio,
    company,
    location,
    email,
    isBountyHunter,
    isCampusExpert,
    isDeveloperProgramMember,
    isEmployee,
    isHireable,
    isSiteAdmin,
    viewerIsFollowing,
    viewerCanFollow,
    createdAt,
    updatedAt
    } }"",
   ""variables"" : null

}";
            GitHubResponse<Common> result = new GitHubResponse<Common>();
            GitHubResponse<ViewerResponse> response = await QueryAsync<ViewerResponse>(query);
            if (response != null)
            {
                result.Headers = response.Headers;
                result.Response = new Common()
                {
                    Id = response.Response.Data.Viewer.Id,
                    DatabaseId = response.Response.Data.Viewer.DatabaseId,
                    AvatarUri = response.Response.Data.Viewer.AvatarUrl,
                    Bio = response.Response.Data.Viewer.Bio,
                    Company = response.Response.Data.Viewer.Company,
                    EmailAddress = response.Response.Data.Viewer.Email,
                    IsAbleToFollow = response.Response.Data.Viewer.IsAbleToFollow,
                    IsBountyHunter = response.Response.Data.Viewer.IsBountyHunter,
                    IsCampusExpert = response.Response.Data.Viewer.IsCampusExpert,
                    IsDeveloperProgramMember = response.Response.Data.Viewer.IsDeveloperProgramMember,
                    IsEmployee = response.Response.Data.Viewer.IsEmployee,
                    IsFollowing = response.Response.Data.Viewer.IsFollowing,
                    IsHireable = response.Response.Data.Viewer.IsHireable,
                    IsSiteAdmin = response.Response.Data.Viewer.IsSiteAdmin,
                    Location = response.Response.Data.Viewer.Location,
                    Login = response.Response.Data.Viewer.Login,
                    Name = response.Response.Data.Viewer.Name,
                    ResourcePath = response.Response.Data.Viewer.ResourcePath,
                    Url = response.Response.Data.Viewer.Url,
                    WebsiteUrl = response.Response.Data.Viewer.WebsiteUrl,
                    Created = response.Response.Data.Viewer.CreatedAt,
                    Updated = response.Response.Data.Viewer.UpdatedAt
                };
            }
            return result;
        }
    }
}
