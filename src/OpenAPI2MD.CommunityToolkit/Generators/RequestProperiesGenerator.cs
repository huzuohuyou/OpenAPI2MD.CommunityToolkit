using Microsoft.OpenApi.Models;

namespace OpenAPI2MD.CommunityToolkit.Generators
{
    public class RequestProperiesGenerator
    {
        private string IndentChar = "····";
        private  int times=0;
        public List<RequestBody> Schemata { get; set; } = new();

        public List<RequestBody> Excute(OpenApiSchema schema)
        {
            if (Equals(null, schema))
                return default;
            InitEntity(schema, Schemata, times);
            return Schemata;
        }

        private string IndentStr(int t)
        {
            var temp = string.Empty;
            for (int i = 0; i < t-1 ; i++)
            {
                temp += IndentChar;
            }
            return temp;
        }

        public void InitEntity(OpenApiSchema schema, List<RequestBody> schematas,int IndentTime)
        {
            if (Equals(schema?.Type, "array"))
            {
                IndentTime++;
                if (Equals(schema.Reference, null))
                {
                    schematas.Add(new RequestBody()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{schema.Items.Reference.Id}",
                        PropertyType = $@"{schema.Type}:{schema.Items.Reference.Id}",
                        Description = schema.Description,
                        IsRequired = schema.Required.Contains(schema.Items.Reference.Id) ? "Y" : "",
                        //Example = (schema.Example == null ? default : (schema.Example as dynamic).Value)?.ToString()
                    });
                }

                IndentTime++;
                schema.Items.Properties.ToList().ForEach(prop =>
                {
                    schematas.Add(new RequestBody()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                        PropertyType = prop.Value.Type == "array"?
                            $@"{prop.Value.Type}:{prop.Value.Items.Reference.Id}"
                            :(prop.Value.Type == "object" ? 
                                $@"{prop.Value.Type}:{prop.Value?.Reference?.Id}"
                                : prop.Value.Type) ,
                        Description = prop.Value.Description,
                        IsRequired = schema.Required.Contains(prop.Key) ? "Y" : "",
                        //Example = (schema.Example == null ? default : (schema.Example as dynamic).Value)?.ToString()
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
                schematas.Add(new RequestBody()
                {
                    PropertyName = $@"{IndentStr(IndentTime)}{schema.Reference.Id}",
                    PropertyType = schema.Type == "object"
                        ? $@"{schema.Type}:{schema?.Reference?.Id}"
                        : schema.Type,
                    IsRequired = schema.Required.Contains(schema.Reference.Id) ? "Y" : "",
                    //Remark = schema.Description,
                });
                IndentTime++;
                schema.Properties.ToList().ForEach(prop =>
                {
                    schematas.Add(new RequestBody()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                        PropertyType = prop.Value.Type == "object"
                            ? $@"{prop.Value.Type}:{prop.Value?.Reference?.Id}"
                            : prop.Value.Type,
                        Description = prop.Value.Description,
                        IsRequired = schema.Required.Contains(prop.Key)?"Y":"",
                        //Example = (prop.Value.Example == null ? default : (prop.Value.Example as dynamic).Value)?.ToString()
                    });
                   
                    InitEntity(prop.Value, schematas, IndentTime);
                });
            }
            
        }
    }
}
