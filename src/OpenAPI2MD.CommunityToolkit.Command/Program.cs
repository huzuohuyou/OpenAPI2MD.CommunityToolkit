// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;
using OpenAPI2MD.CommunityToolkit.Command;
using OpenAPI2MD.CommunityToolkit.Generators;

Console.WriteLine("Hello, World!");
string? jsonPath;
if (args.Any())//cmd传参
    jsonPath = args[0];
else
{
    Console.WriteLine("请输入swagger.json的url:");
    jsonPath = Console.ReadLine();
}
var savePath = new CmdRunner().PrintDoc("cd");
_ = new OpenApimdGenerator().ReadYaml(jsonPath, savePath).Result;
if (File.Exists(Path.Combine(savePath,"swagger.md")))
    Console.WriteLine("文档生成成功!");
