using System;

namespace Core.Http.Client
{
    public class AbpRemoteServiceOptions
    {
        public RemoteServiceConfigurationDictionary RemoteServices { get; set; }

        public AbpRemoteServiceOptions()
        {
            RemoteServices = new RemoteServiceConfigurationDictionary();
        }
    }
}
