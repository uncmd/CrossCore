﻿using Abp.Modules;
using Core.Http.ProxyScripting.Configuration;
using Core.Http.ProxyScripting.Generators.JQuery;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Http
{
    public class AbpHttpModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<AbpApiProxyScriptingOptions>();
        }

        public override void Initialize()
        {
            Configuration.Get<AbpApiProxyScriptingOptions>()
                .Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
