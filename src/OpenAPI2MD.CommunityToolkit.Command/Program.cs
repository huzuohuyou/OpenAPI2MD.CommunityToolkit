// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;
using OpenAPI2MD.CommunityToolkit.Command;
using OpenAPI2MD.CommunityToolkit.Generators;

Console.WriteLine("Hello, World!");
string? jsonPath="";
if (args.Any())//cmd传参
    jsonPath = args[0];
//else
//{
//    Console.WriteLine("请输入swagger.json的url:");
//    jsonPath = Console.ReadLine();
//}
var savePath = new CmdRunner().PrintDoc("cd");

await new OpenAPI2Word.CommunityToolkit.Generators.OpenApimdGenerator().ReadYaml(jsonPath, savePath);
if (File.Exists(Path.Combine(savePath,"swagger.md")))
    Console.WriteLine("文档生成成功!");
