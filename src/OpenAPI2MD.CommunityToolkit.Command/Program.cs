// See https://aka.ms/new-console-template for more information

using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.OpenXmlFormats;
using OpenAPI2MD.CommunityToolkit.Command;

Console.WriteLine("Hello, World!");


#if DEBUG
//var swagger = "http://172.26.172.122:18100/swagger/2.4.0/swagger.json";
//Console.WriteLine(swagger);
//var savePath = new CmdRunner().PrintDoc("cd");
//await new OpenAPI2MD.CommunityToolkit.Generators.OpenApiMdGenerator().Generate(swagger, savePath).ConfigureAwait(false);
//if (File.Exists(Path.Combine(savePath, "swagger.md")))
//    Console.WriteLine($"{Path.Combine(savePath, "swagger.md")}markdown生成成功!");
#endif

var fileOption = new Option<string>(
    name: "--type",
    description: "The file type to generate from swagger.json .");

var swaggerOption = new Option<string>(
    name: "--swagger",
    description: "The swagger json string path or url");

var outputOption = new Option<string>(
    name: "--output",
    description: "The doc output path");

var rootCommand = new RootCommand("Sample app for Generate Markdown，Word from swagger.json")
{
    swaggerOption,fileOption,outputOption
};


rootCommand.SetHandler(async (fileType,swagger,output) => { await GenerateDoc(fileType, swagger,output); }, fileOption, swaggerOption, outputOption);

return await rootCommand.InvokeAsync(args);

static async Task GenerateDoc(string fileType,string swagger,string output)
{
    if (string.IsNullOrWhiteSpace(fileType))
    {
        Console.WriteLine("请输入生成文件类型；md|word");
        fileType = Console.ReadLine();
    }
    if (string.IsNullOrWhiteSpace(swagger))
    {
        Console.WriteLine("请输入swagger.json路径，支持在线地址，本地文件");
        swagger = Console.ReadLine();
    }
   
    if (string.IsNullOrWhiteSpace(output))
    {
        output= new CmdRunner().PrintDoc("cd");
    }
    else if(!Directory.Exists(output))
    {
        output:
        Console.WriteLine("请输入正确的保存路径");
        output = Console.ReadLine();
        if (!Directory.Exists(output))
            goto output;
    }
    Console.WriteLine($"type:{fileType}\nswagger: {swagger}");
    var outputFile = "";
    if (Equals(fileType, "md"))
    {
        outputFile = await new OpenAPI2MD.CommunityToolkit.Generators.OpenApiMdGenerator().Generate(swagger, output);
    }
    else if (Equals(fileType, "word"))
    {
        outputFile = await new OpenAPI2Word.CommunityToolkit.Generators.OpenApiWordGenerator().Generate(swagger, output);
    }
    Console.WriteLine($"output:{outputFile}!");
}



