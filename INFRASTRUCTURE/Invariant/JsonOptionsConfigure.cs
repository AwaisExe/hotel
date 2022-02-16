using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Invariant
{
    public class JsonOptionsConfigure
    {
        public static void ConfigureJsonOptions(MvcNewtonsoftJsonOptions jsonOptions)
        {
            jsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
