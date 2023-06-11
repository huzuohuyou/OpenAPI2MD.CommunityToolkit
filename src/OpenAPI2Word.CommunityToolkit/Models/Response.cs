﻿namespace OpenAPI2Word.CommunityToolkit.Models;

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
        responseRow.GetCell(0).SetColumCellText(Code ?? "");
        responseRow.GetCell(1).SetColumCellText(Des ?? "");
        responseRow.GetCell(2).SetColumCellText(ResponseType ?? "");
        responseRow.GetCell(3).SetText3(ResponseDataType ?? "");
        responseRow.MergeCells(3, 5);
        var resonseFieldRow = table.CreateRow();
        resonseFieldRow.GetCell(0).SetText3("返回属性名" ?? "");
        resonseFieldRow.MergeCells(0, 1);
        resonseFieldRow.GetCell(1).SetText3("数据类型" ?? "");
        resonseFieldRow.MergeCells(1, 2);
        resonseFieldRow.GetCell(2).SetText3("说明" ?? "");
        resonseFieldRow.MergeCells(2, 3);
        var sindex = 0;
        Schemas.ForEach(r =>
        {
            r.Generate(table, sindex);
            sindex++;
        });


        var example = new ExampleValueGenerator().Excute(OpenApiSchema);
        var exampleRow = table.CreateRow();
        exampleRow.GetCell(0).SetText3("示例" ?? "");
        exampleRow.GetCell(1).SetJsonOrBrText(example ?? "");
        exampleRow.MergeCells(1, 5);
    }
}