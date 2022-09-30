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

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/")
            };

            var stream = await httpClient.GetStreamAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openapi.yaml"));

            // Read V3 as YAML
            var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

            // Write V2 as JSON
            var outputString = openApiDocument.Serialize(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

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