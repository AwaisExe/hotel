using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Invariant
{
    public sealed class AspNet
    {
        public sealed class Mvc
        {
            public const string ClearTemplate = "";
            public const string Action = "action";
            public const string ActionTemplate = "[action]";
            public const string Controller = "controller";
            public const string ControllerTemplate = "[controller]";
            public const string DefaultControllerTemplate = "api/v{version:apiVersion}/" + ControllerTemplate;
        }
    }
}
