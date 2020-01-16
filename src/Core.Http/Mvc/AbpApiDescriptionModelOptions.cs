using Abp.Application.Services;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Mvc
{
    public class AbpApiDescriptionModelOptions
    {
        public HashSet<Type> IgnoredInterfaces { get; }

        public AbpApiDescriptionModelOptions()
        {
            IgnoredInterfaces = new HashSet<Type>
            {
                typeof(ITransientDependency),
                typeof(ISingletonDependency),
                typeof(IDisposable),
                typeof(IAvoidDuplicateCrossCuttingConcerns)
            };
        }
    }
}
