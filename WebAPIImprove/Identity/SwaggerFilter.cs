using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPIImprove.Identity
{
    public class SwaggerFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var authorised = apiDescription.GetControllerAndActionAttributes<AuthorizeAttribute>().Any();
            if (authorised)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();
                operation.parameters.Add(new Parameter()
                {
                    name = "Authorization",
                    @in = "header",
                    description = "bearer token",
                    required = true,
                    type = "string"
                });
            }
        }
    }
}