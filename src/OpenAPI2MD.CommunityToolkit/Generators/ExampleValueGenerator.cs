using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPI2MD.CommunityToolkit.Generators
{
    public enum ResponseType{
        array,
        obj,
        basetype
    }
    public class ExampleValueGenerator
    {
       public ResponseType ResponseType { get; set; }
        bool ok = false;
        public List<Dictionary<string, object>> ArraySheme { get; set; } = new();
        public Dictionary<string, object> ObjectSheme { get; set; } = new();
        public object BaseTypeSheme { get; set; } = new();
        public string Excute(OpenApiSchema schema)
        {
            var r= InitEntity(schema);
            if (r!=null)
                return ConvertJsonString(JsonConvert.SerializeObject(r));
            return default;
        }

        private object InitEntity(OpenApiSchema schema)
        {
            if (Equals(schema.Type,"array"))
            {
                var temp = new List<object>();
                temp.Add(InitEntity(schema.Items));
                return temp;
            }
            else if (Equals(schema.Type,"object"))
            {
                var temp =  new Dictionary<string, object>();
                schema.Properties.Keys.ToList().ForEach(r =>
                {
                    temp.Add(r, InitEntity(schema.Properties[r]));
                });
                return temp;
            }
            else
            {//基本类型
                if (Equals(schema.Type, "integer"))
                {
                    int result;
                    int.TryParse((schema.Example == null ? default : (schema.Example as dynamic).Value)?.ToString(), out result);
                    return result;
                }
                else if (Equals(schema.Type, "number"))
                {
                    double result;
                    double.TryParse((schema.Example == null ? default : (schema.Example as dynamic).Value)?.ToString(), out result);
                    return result;
                }
                else if (Equals(schema.Type, "boolean"))
                {
                    bool result;
                    bool.TryParse((schema.Example == null ? default : (schema.Example as dynamic).Value)?.ToString(), out result);
                    return result;
                }
                else
                    return schema.Example == null ? default : (schema.Example as dynamic).Value;
            }
        }

        private string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = '-'
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}
