using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Swagger.Filters
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            var result = from a in context.ApiDescription.ParameterDescriptions
                         join b in operation.Parameters.OfType<NonBodyParameter>() on a.Name equals b?.Name
                         where a.ModelMetadata.ModelType == typeof(IFormFile)
                         select b;


            result.ToList().ForEach(x =>
            {
                x.In = "formData";
                x.Type = "file";
            });
        }
    }
}
