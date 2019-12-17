using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client.DynamicProxying
{
    public class ApiVersionInfo  //TODO: Rename to not conflict with api versioning apis
    {
        public string BindingSource { get; }
        public string Version { get; }

        public ApiVersionInfo(string bindingSource, string version)
        {
            BindingSource = bindingSource;
            Version = version;
        }

        public bool ShouldSendInQueryString()
        {
            //TODO: Constant! TODO: Other sources!
            return !BindingSource.IsIn("Path");
        }
    }
}
