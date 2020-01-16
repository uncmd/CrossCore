using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client.IdentityModel
{
    public interface IAbpIdentityClientOptions
    {
        IdentityClientConfigurationDictionary IdentityClients { get; set; }
    }
}
