using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Http.Mvc
{
    public class ConventionalControllerSettingList : List<ConventionalControllerSetting>
    {
        [CanBeNull]
        public ConventionalControllerSetting GetSettingOrNull(Type controllerType)
        {
            return this.FirstOrDefault(controllerSetting => controllerSetting.ControllerTypes.Contains(controllerType));
        }
    }
}
