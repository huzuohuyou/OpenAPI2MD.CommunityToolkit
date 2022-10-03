using Grynwald.MarkdownGenerator;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;

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

            var stream = await _client.GetStreamAsync("/swagger/v1/swagger.json");

            var openApiDocument = new OpenApiStreamReader(new OpenApiReaderSettings() { });
            var temp = openApiDocument.Read(stream, out var diagnostic);

            var lines=File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openapi.yaml"));
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