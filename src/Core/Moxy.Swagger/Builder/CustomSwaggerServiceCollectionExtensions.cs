using Microsoft.Extensions.DependencyInjection;
using Moxy.Swagger.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Swagger.Builder
{
    public static class CustomSwaggerServiceCollectionExtensions
    {

        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services, CustsomSwaggerOptions options)
        {
            services.AddSwaggerGen(c =>
             {
                 if (options.ApiVersions == null) return;
                 foreach (var version in options.ApiVersions)
                 {
                     c.SwaggerDoc(version, new Info { Title = options.ProjectName, Version = version });
                 }
                 c.TagActionsBy(s =>
                 {
                     if (string.IsNullOrEmpty(s.ActionDescriptor.RouteValues["area"]))
                     {
                         return s.ActionDescriptor.RouteValues["controller"];
                     }
                     return s.ActionDescriptor.RouteValues["area"] + "_" + s.ActionDescriptor.RouteValues["controller"].ToLower();
                 });
                 c.OperationFilter<SwaggerDefaultValueFilter>();
                 options.AddSwaggerGenAction?.Invoke(c);

             });
            return services;
        }
    }
}
