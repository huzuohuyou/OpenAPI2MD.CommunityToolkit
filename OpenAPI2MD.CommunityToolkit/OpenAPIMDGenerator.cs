namespace OpenAPI2MD.CommunityToolkit
{

    public class OpenAPIMDGenerator
    {
        public  string ReadYaml()
        {
            var lines=File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openapi.yaml"));
            return string.Empty;
        }
    }

}