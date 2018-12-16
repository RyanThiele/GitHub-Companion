using System;
using System.Collections.Generic;

namespace GitHubCompanion.Models
{
    public class Authorization
    {
        public Headers.GitHubHeaders Headers { get; set; }
        public int Id { get; set; }
        public Uri Url { get; set; }
        public Application App { get; set; }
        public string Token { get; set; }
        public string Hashed_Token { get; set; }
        public long Token_Last_Eight { get; set; }
        public string Note { get; set; }
        public Uri Note_Url { get; set; }

        public IEnumerable<string> Scopes { get; set; }
    }

    public class Application
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Client_Id { get; set; }
    }

}
