using System.Collections.Generic;
using System.IO;

namespace GitHubCompanion.Tests
{
    public class AuthorizeDataRow
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class DataSource
    {
        private static readonly List<AuthorizeDataRow> _authorizeDataRows = new List<AuthorizeDataRow>();

        public static IEnumerable<object[]> AuthorizeTestData
        {
            get
            {
                List<object[]> rows = new List<object[]>();
                int currentRow = 0;
                using (FileStream fileStream = new FileStream("Authorize.cvsd", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (reader.Peek() != -1)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');

                            rows.Add(new object[] { currentRow, values[0], values[1] });
                        }
                    }
                }

                return rows;
            }
        }
    }
}
