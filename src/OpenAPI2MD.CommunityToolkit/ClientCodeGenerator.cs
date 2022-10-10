using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace OpenAPI2MD.CommunityToolkit
{
    public class ClientCodeGenerator
    {
        public async Task Excute()
        {

            System.Net.WebClient wclient = new System.Net.WebClient();
            var document = await OpenApiDocument.FromJsonAsync(wclient.DownloadString("http://172.26.176.171:18100/swagger/2.1.4/swagger.json"));
            wclient.Dispose();
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = "MyClass",
                CSharpGeneratorSettings =
    {
        Namespace = "MyNamespace"
    }
            };

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
        }
    }
}
