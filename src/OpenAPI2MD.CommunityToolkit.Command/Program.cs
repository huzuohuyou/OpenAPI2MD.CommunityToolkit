// See https://aka.ms/new-console-template for more information

using OpenAPI2MD.CommunityToolkit.Generators;

Console.WriteLine("Hello, World!");
var jsonPath = string.Empty;
if (args.Any())//cmd传参
    jsonPath = args[0];
else
{
    Console.WriteLine("请输入swagger.json的url:");
    jsonPath = Console.ReadLine();
}
    
_ = new OpenApimdGenerator().ReadYaml(jsonPath).Result;
