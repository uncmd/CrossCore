using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client.IdentityModel
{
    public class AbpIdentityClientOptions
    {
        public IdentityClientConfigurationDictionary IdentityClients { get; set; }

        public AbpIdentityClientOptions()
        {
            IdentityClients = new IdentityClientConfigurationDictionary();
        }
    }
}
