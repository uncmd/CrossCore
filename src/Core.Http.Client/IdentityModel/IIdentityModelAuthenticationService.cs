using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Client.IdentityModel
{
    public interface IIdentityModelAuthenticationService
    {
        Task<bool> TryAuthenticateAsync(
            [NotNull] HttpClient client,
            string identityClientName = null);

        Task<string> GetAccessTokenAsync(
            IdentityClientConfiguration configuration);
    }
}
