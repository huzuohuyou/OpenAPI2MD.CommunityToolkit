using Grynwald.MarkdownGenerator;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json;
using OpenAPI2MD.CommunityToolkit.Models;
using System.Text;
using System.Text.Json;

namespace OpenAPI2MD.CommunityToolkit
{

    public class OpenAPIMDGenerator
    {
        public async Task<string> ReadYaml()
        {

            var _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:18100/")
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
       
            var stream = await _client.GetStreamAsync("/swagger/1.0.0/swagger.json");
            var doc = new OpenApiStreamReader().Read(stream, out var diagnostic);
            var s = doc.Paths.Last().Value.Operations.Values;


            var document = new MdDocument();
            var sb = new StringBuilder();
            // add a heading and a paragraph to the root block
            document.Root.Add(new MdHeading($@"{doc.Info.Title}({doc.Info.Version})", 1));
            sb.Append($@"# {doc.Info.Title}({doc.Info.Version}) 
");
            var tag = string.Empty;
            doc.Paths.ToList().ForEach(r => {
                var operation = r.Value.Operations.Values.FirstOrDefault();
                if (!tag.Equals(operation.Tags.FirstOrDefault().Name))
                {
                    tag = operation.Tags.FirstOrDefault().Name;
                    document.Root.Add(new MdHeading(tag, 2));
                }
                var s = new PathTable().ToString();
                document.Root.Add(new MdHeading(operation.OperationId, 3));
                document.Root.Add(new MdParagraph(s));
                sb.Append($@"## {operation.OperationId} 
");
                sb.Append($@"{s} 
");

            });
            document.Root.Blocks.ToList().ForEach(r =>
            {
                
            });
            document.Root.Add(new MdParagraph("Hello world!"));
            File.WriteAllText("dev.md", sb.ToString());
            // save document to a file
            document.Save("HelloWorld.md");

            return string.Empty;
        }
    }

}