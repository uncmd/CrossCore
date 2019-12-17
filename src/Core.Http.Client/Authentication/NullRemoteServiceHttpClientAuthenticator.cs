using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Client.Authentication
{
    public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
    {
        public Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context)
        {
            return Task.CompletedTask;
        }
    }
}
