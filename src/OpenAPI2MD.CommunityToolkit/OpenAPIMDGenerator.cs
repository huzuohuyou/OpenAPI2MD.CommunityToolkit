using Grynwald.MarkdownGenerator;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json;
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
            var json = await _client.GetStringAsync("/swagger/1.0.0/swagger.json");

             var jsonDocument = JsonDocument.Parse(json);
            var s = jsonDocument.Deserialize<OpenApiDocument>();
            var paths = jsonDocument.RootElement.GetProperty("paths");
            // var doc0=System.Text.Json.JsonSerializer.Deserialize<OpenApiDocument>(json, options);
            var stream = await _client.GetStreamAsync("/swagger/1.0.0/swagger.json");
            var doc = new OpenApiStreamReader().Read(stream, out var diagnostic);
            var url = "https://localhost:18100/swagger/1.0.0/swagger.json";
            var document2 = await NSwag.OpenApiDocument.FromUrlAsync(url);
            //var lines=File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openapi.yaml"));
            // create the document (initially empty)
            var document = new MdDocument();

            // add a heading and a paragraph to the root block
            document.Root.Add(new MdHeading("Heading", 1));
            document.Root.Add(new MdParagraph("Hello world!"));

            // save document to a file
            document.Save("HelloWorld.md");

            return string.Empty;
        }
    }

}