using INFRASTRUCTURE.Invariant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace INFRASTRUCTURE.Swagger
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddAppApiVersioning(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(StartUp.MajorVersion1, StartUp.MinorVersion0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (hostingEnvironment == null) throw new ArgumentNullException(nameof(hostingEnvironment));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Swagger.DocVersions.v1_0, CreateInfoForApiVersion(configuration, Swagger.DocVersions.v1_0));
                c.OperationFilter<RemoveVersionFromParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = apiDesc.CustomAttributes()
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == docName)
                           && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == docName));
                });

                c.OperationFilter<SwaggerDefaultValuesFilter>();
                c.DescribeAllParametersInCamelCase();

            });

            return services;
        }

        private static OpenApiInfo CreateInfoForApiVersion(IConfiguration configuration, string version)
        {
            var info = new OpenApiInfo
            {
                Version = version,
                Title = Swagger.Info.Title,
                Description = Swagger.Info.Description,
                Contact = new OpenApiContact
                {
                    Name = Swagger.Info.ContactName,
                    Email = Swagger.Info.ContactEmail
                }
            };

            return info;
        }
    }
}
