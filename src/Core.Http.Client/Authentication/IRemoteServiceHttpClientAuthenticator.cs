using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Client.Authentication
{
    public interface IRemoteServiceHttpClientAuthenticator
    {
        Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context);
    }
}
