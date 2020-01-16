using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Http.Mvc
{
    public static class ActionResultHelper
    {
        public static List<Type> ObjectResultTypes { get; }

        static ActionResultHelper()
        {
            ObjectResultTypes = new List<Type>
            {
                //typeof(JsonResult),
                typeof(ObjectResult),
                typeof(NoContentResult)
            };
        }

        public static bool IsObjectResult(Type returnType)
        {
            returnType = UnwrapTask(returnType);

            if (!typeof(IActionResult).IsAssignableFrom(returnType))
            {
                return true;
            }

            return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
        }

        public static Type UnwrapTask([NotNull] Type type)
        {
            if (type == typeof(Task))
            {
                return typeof(void);
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return type.GenericTypeArguments[0];
            }

            return type;
        }
    }
}
