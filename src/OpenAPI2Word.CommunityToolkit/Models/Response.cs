using System.Text;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OpenAPI2Word.CommunityToolkit.Extensions;
using OpenAPI2Word.CommunityToolkit.Generators;

namespace OpenAPI2Word.CommunityToolkit.Models;

public class Response
{
    public string? Code { get; set; }
    public string? Des { get; set; }
    public string? Remark { get; set; }
    public string? ResponseType { get; set; }
    public string? ResponseDataType { get; set; }
    public List<Schema> Schemas { get; set; } = new();
    public OpenApiSchema? OpenApiSchema { get; set; }
    public void Generate(XWPFTable table)
    {
        var responseRow = table.CreateRow();
        responseRow.GetCell(0).SetText(Code ?? "");
        responseRow.GetCell(1).SetText(Des ?? "");
        responseRow.GetCell(2).SetText(ResponseType ?? "");
        responseRow.GetCell(3).SetText(ResponseDataType ?? "");
        responseRow.MergeCells(3,5);
        var resonseFieldRow = table.CreateRow();
        resonseFieldRow.GetCell(0).SetText("返回属性名" ?? "");
        resonseFieldRow.GetCell(1).SetText("数据类型" ?? "");
        resonseFieldRow.GetCell(2).SetText("说明" ?? "");
        resonseFieldRow.MergeCells(2,5);
        Schemas.ForEach(r =>
        {
            r.Generate(table);
        });
        

        var example = new ExampleValueGenerator().Excute(OpenApiSchema);
        var exampleRow = table.CreateRow();
        exampleRow.GetCell(0).SetText("示例" ?? "");
        exampleRow.GetCell(1).SetText2(example ?? "");
        exampleRow.MergeCells(1, 5);
    }
}