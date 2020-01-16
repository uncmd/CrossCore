using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Mvc
{
    public class AbpAspNetCoreMvcOptions
    {
        public AbpConventionalControllerOptions ConventionalControllers { get; }

        public AbpAspNetCoreMvcOptions()
        {
            ConventionalControllers = new AbpConventionalControllerOptions();
        }
    }
}
