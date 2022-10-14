using Microsoft.OpenApi.Models;

namespace OpenAPI2MD.CommunityToolkit.Generators
{
    public class ResponseProperiesGenerator
    {
        private string IndentChar = "····";
        private  int times=0;
        private List<string> ReferenceIds=new List<string>();
        public List<Schema> Schemata { get; set; } = new();
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
        public List<Schema> Excute(OpenApiSchema schema)
        {
            if (Equals(null, schema))
                 return default;
            InitEntity(schema, Schemata, times);
            return Schemata.Count>0?Schemata: new List<Schema>() { new Schema() { PropertyName = "_", PropertyType = schema.Type, Description = schema.Description } };
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

        public void InitEntity(OpenApiSchema schema, List<Schema> schematas,int IndentTime)
        {
            if (Equals(schema?.Type, "array"))
            {
                IndentTime++;
                if (Equals(schema.Reference, null))
                {
                    schematas.Add(new Schema()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{schema.Items.Reference?.Id}",
                        PropertyType = GetSchemaType(schema),
                        Description = schema.Description,
                    });
                    ReferenceIds.Add(schematas.Last().PropertyName.Trim('·'));
                }

                IndentTime++;
                schema.Items.Properties.ToList().ForEach(prop =>
                {
                    schematas.Add(new Schema()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                        PropertyType = GetSchemaType(prop.Value), 
                        Description = prop.Value.Description,
                    });
                    ReferenceIds.Add(schematas.Last().PropertyName.Trim('·'));
                    if (ReferenceIds.Count(r=>r == schematas.Last().PropertyName.Trim('·'))<2)
                    {
                        if (prop.Value.Type == "array")
                            InitEntity(prop.Value.Items, schematas, IndentTime);
                        else
                            InitEntity(prop.Value, schematas, IndentTime);
                    }
                        
                });
            }
            else if (Equals(schema?.Type, "object"))
            {
                //IndentTime++;
                //schematas.Add(new Schema()
                //{
                //    PropertyName = $@"{IndentStr(IndentTime)}{schema.Reference.Id}",
                //    PropertyType = schema.Type == "object"
                //        ? $@"{schema.Type}:{schema?.Reference?.Id}"
                //        : schema.Type,
                //    Remark = schema.Description,
                //});
                IndentTime++;
                schema.Properties.ToList().ForEach(prop =>
                {
                    schematas.Add(new Schema()
                    {
                        PropertyName = $@"{IndentStr(IndentTime)}{prop.Key}",
                        PropertyType = GetSchemaType(prop.Value),
                        Description = prop.Value.Description,
                    });
                    ReferenceIds.Add(schematas.Last().PropertyName.Trim('·'));
                    if (ReferenceIds.Count(r => r == schematas.Last().PropertyName.Trim('·')) < 2)
                        InitEntity(prop.Value, schematas, IndentTime);

                });
            }
            
        }
    }
}
