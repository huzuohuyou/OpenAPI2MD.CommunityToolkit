// See https://aka.ms/new-console-template for more information

using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using OpenAPI2MD.CommunityToolkit.Command;

Console.WriteLine("Hello, World!");


var mdOption = new Option<string>(
    name: "--md",
    description: "The file type to generate from swagger.json .");

var wordOption = new Option<string>(
    name: "--word",
    description: "The file type to generate from swagger.json .");

var rootCommand = new RootCommand("Sample app for Generate Markdown，Word from swagger.json");
rootCommand.AddOption(mdOption);
rootCommand.AddOption(wordOption);

rootCommand.SetHandler((swagger) => { GenerateWord(swagger!); }, wordOption);
rootCommand.SetHandler((swagger) => { GenerateMd(swagger!); }, mdOption);

return await rootCommand.InvokeAsync(args);

static void GenerateMd(string swagger)
{
    swagger = "http://172.26.172.122:18100/swagger/2.4.0/swagger.json";
    Console.WriteLine(swagger);
    var savePath = new CmdRunner().PrintDoc("cd");
     new OpenAPI2MD.CommunityToolkit.Generators.OpenApiMdGenerator().Generate(swagger, savePath).ConfigureAwait(false);
    if (File.Exists(Path.Combine(savePath, "swagger.md")))
        Console.WriteLine($"{Path.Combine(savePath, "swagger.md")}markdown生成成功!");
}

static void GenerateWord (string swagger)
{
    var savePath = new CmdRunner().PrintDoc("cd");
    new OpenAPI2Word.CommunityToolkit.Generators.OpenApiWordGenerator().ReadSwagger(swagger, savePath).ConfigureAwait(false);
    if (File.Exists(Path.Combine(savePath, "swagger.md")))
        Console.WriteLine("word生成成功!");
}