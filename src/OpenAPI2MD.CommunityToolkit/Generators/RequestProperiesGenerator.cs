using Microsoft.OpenApi.Models;

namespace OpenAPI2MD.CommunityToolkit.Generators
{
    public class RequestProperiesGenerator
    {
        private string IndentChar = "····";
        private int times = 0;

        private string GetSchemaType(OpenApiSchema schema)
        {
            if (schema.Type == "array" && schema.Items?.Reference != null)
                return $@"{schema.Type}:{schema.Items?.Reference.Id}";
            if (schema.Type == "array" && schema.Items?.Reference == null)
                return $@"{schema.Type}:{schema.Items?.Type}";
            if (schema.Type == "object" && schema?.Reference != null)
                return $@"{schema.Type}:{schema?.Reference.Id}";
            return schema.Type;

        }
        public List<RequestBody> Schemata { get; set; } = new();

        public List<RequestBody> Excute(OpenApiSchema schema)
        {
            if (Equals(null, schema))
                return default;
            InitEntity(schema, Schemata, times);
            return Schemata.Count > 0 ? Schemata : new List<RequestBody>() { new RequestBody() { PropertyName = "_", PropertyType = schema.Type, Description = schema.Description } }; ;
        }

        private string IndentStr(int t)
        {
            var temp = string.Empty;
            for (int i = 0; i < t - 1; i++)
            {
                temp += IndentChar;
            }
            return temp;
        }



        public void InitEntity(OpenApiSchema schema, List<RequestBody> schematas, int IndentTime)
        {
            if (Equals(schema?.Type, "array"))
            {
                IndentTime++;
                if (!string.IsNullOrWhiteSpace(schema.Items?.Reference?.Id))
                    schematas.Add(new RequestBody()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{schema.Items?.Reference?.Id}",
                        PropertyType = GetSchemaType(schema),
                        Description = schema.Description,
                        IsRequired = schema.Required != null && schema.Required.Contains(schema.Items?.Reference?.Id) ? "Y" : "",
                    });

                IndentTime++;
                schema.Items.Properties.ToList().ForEach(prop =>
                {
                    if (!string.IsNullOrWhiteSpace(prop.Key))
                        schematas.Add(new RequestBody()
                        {
                            PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                            PropertyType = GetSchemaType(prop.Value),
                            Description = prop.Value.Description,
                            IsRequired = schema.Required.Contains(prop.Key) ? "Y" : "",
                        });
                    if (prop.Value.Type == "array")
                        InitEntity(prop.Value.Items, schematas, IndentTime);
                    else
                        InitEntity(prop.Value, schematas, IndentTime);
                });
            }
            else if (Equals(schema?.Type, "object"))
            {
                IndentTime++;
                if (!string.IsNullOrWhiteSpace(schema.Reference.Id))
                    schematas.Add(new RequestBody()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{schema.Reference.Id}",
                        PropertyType = GetSchemaType(schema),
                        IsRequired = schema.Required.Contains(schema.Reference.Id) ? "Y" : "",
                    });
                IndentTime++;
                schema.Properties.ToList().ForEach(prop =>
                {
                    if (!string.IsNullOrWhiteSpace(prop.Key))
                        schematas.Add(new RequestBody()
                        {
                            PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                            PropertyType = GetSchemaType(prop.Value),
                            Description = prop.Value.Description,
                            IsRequired = schema.Required.Contains(prop.Key) ? "Y" : "",
                        });

                    InitEntity(prop.Value, schematas, IndentTime);
                });
            }
            else
            {//基本类型

                //schematas.Add(new RequestBody()
                //{
                //    PropertyName = $@"{IndentStr(IndentTime)}_",
                //    PropertyType = schema.Type,
                //    Description = schema.Description,
                //    //IsRequired = schema.Required ? "Y" : "",
                //});

            }

        }
    }
}
