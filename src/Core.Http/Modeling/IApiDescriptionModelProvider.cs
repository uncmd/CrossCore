using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Modeling
{
    public interface IApiDescriptionModelProvider
    {
        ApplicationApiDescriptionModel CreateApiModel();
    }
}
