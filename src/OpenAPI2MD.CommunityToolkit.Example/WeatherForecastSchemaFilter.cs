using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenAPI2MD.CommunityToolkit.Example
{
    public class WeatherForecastSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = new OpenApiObject
            {
                ["Id"] = new OpenApiInteger(1),
                ["Description"] = new OpenApiString("An awesome product")
            };
        }
    }
}
