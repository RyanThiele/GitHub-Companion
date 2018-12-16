using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public interface IVariousService
    {
        Task<string> GetZenAsync();
    }
}
