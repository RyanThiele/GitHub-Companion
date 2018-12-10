using System;

namespace GitHubCompanion.Models
{
    public class Profile
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Node_Id { get; set; }
        public Uri Avatar_Url { get; set; }
        public Uri Gravatar_Url { get; set; }
        public Uri Url { get; set; }
        public Uri Html_Url { get; set; }
        public Uri Followers_Url { get; set; }
        public Uri Following_Url { get; set; }
        public Uri Gists_Url { get; set; }
        public Uri Starred_Url { get; set; }
        public Uri Suscriptions_Url { get; set; }
        public Uri Orginizations_Url { get; set; }
        public Uri Repos_Url { get; set; }
        public Uri Events { get; set; }
        public Uri Received_Events_Url { get; set; }
        public string Type { get; set; }
        public bool Site_Admin { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool Hireable { get; set; }
        public string Bio { get; set; }
        public int Public_Repros { get; set; }
        public int Public_Gists { get; set; }
        public int Following { get; set; }

        private string created_At;

        public string Created_At
        {
            get { return created_At; }
            set
            {
                created_At = value;
                DateTime dateTime = new DateTime();
                if (DateTime.TryParse(value, out dateTime)) CreatedDateTime = dateTime;
            }
        }

        private string updated_At;

        public string Updated_At
        {
            get { return updated_At; }
            set
            {
                updated_At = value;
                DateTime dateTime = new DateTime();
                if (DateTime.TryParse(value, out dateTime)) UpdatedDateTime = dateTime;
            }
        }




        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public Headers.GitHubHeaders Headers { get; set; }
    }
}
