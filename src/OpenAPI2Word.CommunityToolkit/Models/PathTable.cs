using NPOI.OpenXmlFormats.Vml;
using OpenAPI2Word.CommunityToolkit.Extensions;
using OpenAPI2Word.CommunityToolkit.Generators;

namespace OpenAPI2Word.CommunityToolkit.Models;

public class PathTable
{
    public string Summary { get; set; }
    public string Name { get; set; }

    private string DisplayDeprecated
    {
        get
        {
            if (Deprecated)
                return $@"<tr>
    <td >方法过时</td>
    <td colspan=""5"" >此方法以后不再维护,或在后续更新中移除；</td>
</tr>";
            return string.Empty;
        }
    }

    private string DeprecatedStyle
    {
        get
        {
            if (Deprecated)
                return @"style=""text-decoration:line-through;""";
            return string.Empty;
        }
    }
    public string Description { get; set; }
    public string Url { get; set; }
    public string RequestMethod { get; set; }
    public string RequestType { get; set; }
    public bool Deprecated { get; set; }
    public string ResponseType { get; set; }
    public List<RequestParam> RequestParams { get; set; } = new();
    public List<RequestBody> RequestBodys { get; set; } = new();
    public OpenApiSchema? RequestBody { get; set; }
    public List<Response> Responses { get; set; } = new();
    public List<Example> Examples { get; set; } = new();

    public void Generate(XWPFDocument doc)
    {

        var p0 = doc.CreateTable(0, 6);//默认有一行
        p0.SetColumnWidth(0, 1000);
        //总结

        var summaryCell = p0.GetRow(0).GetTableCells().FirstOrDefault();
        summaryCell.SetText(Summary);
        p0.GetRow(0).SetColor("#696969");
        p0.GetRow(0).CreateCell();
        p0.GetRow(0).CreateCell();
        p0.GetRow(0).CreateCell();
        p0.GetRow(0).CreateCell();
        p0.GetRow(0).CreateCell();
        //p0.GetRow(0).MergeCells(0, 5);
        //名称
        var nameRow = p0.CreateRow();
        nameRow.GetCell(0).SetText("接口名称");
        nameRow.GetCell(1).SetText(Name);
        nameRow.MergeCells(1, 5);
        //描述
        var descriptionRow = p0.CreateRow();
        descriptionRow.GetCell(0).SetText("接口描述");
        descriptionRow.GetCell(1).SetText2(Description ?? "");//.SetParagraph(new CellParagraph(descriptionRow.GetTable(), Description ?? "").cellParagraph);//.SetText(Description ?? "");
        //URL
        var urlRow = p0.CreateRow();
        urlRow.GetCell(0).SetText("URL");
        urlRow.GetCell(1).SetText(Url ?? "");

        //请求方式
        var requestMethodRow = p0.CreateRow();
        requestMethodRow.GetCell(0).SetText("请求方式");
        requestMethodRow.GetCell(1).SetText(RequestMethod ?? "");

        //请求类型
        var requestTypeRow = p0.CreateRow();
        requestTypeRow.GetCell(0).SetText("请求类型");
        requestTypeRow.GetCell(1).SetText(ResponseType ?? "");

        //HEADER \QUERY 参数头
        var requestParamPropertiesRow = p0.CreateRow();
        requestParamPropertiesRow.GetCell(0).SetText("参数名");
        requestParamPropertiesRow.GetCell(1).SetText("数据类型");
        requestParamPropertiesRow.GetCell(2).SetText("参数类型");
        requestParamPropertiesRow.GetCell(3).SetText("必填");
        requestParamPropertiesRow.GetCell(4).SetText("说明");
        requestParamPropertiesRow.GetCell(5).SetText("示例");

        RequestParams.ForEach(r =>
        {
            r.Generate(p0);
        });

        //BODY 参数头
        var requestBodyPropertiesRow = p0.CreateRow();
        requestBodyPropertiesRow.GetCell(0).SetText("参数名");
        requestBodyPropertiesRow.GetCell(1).SetText("数据类型");
        requestBodyPropertiesRow.GetCell(2).SetText("参数类型");
        requestBodyPropertiesRow.GetCell(3).SetText("必填");
        requestBodyPropertiesRow.GetCell(4).SetText("说明");
        RequestBodys.ForEach(r =>
        {
            r.Generate(p0);
        });

        //BODY 参数头
        var requestBodyExampleRow = p0.CreateRow();
        requestBodyExampleRow.GetCell(0).SetText("示例");
        requestBodyExampleRow.MergeCells(0, 5);
        var requestBody = new ExampleValueGenerator().Excute(RequestBody);
        //BODY Json
        var requestBodyExampleJsonRow = p0.CreateRow();
        requestBodyExampleJsonRow.GetCell(0).SetText(requestBody ?? "");
        requestBodyExampleJsonRow.MergeCells(0, 5);

        Responses.ForEach(r =>
        {
            //BODY 参数头
            var responseRow = p0.CreateRow();
            responseRow.GetCell(0).SetText("状态码");
            responseRow.GetCell(1).SetText("描述");
            responseRow.GetCell(2).SetText("类型");
            responseRow.GetCell(3).SetText("数据类型");
            r.Generate(p0);
            responseRow.MergeCells(3,5);
        });
        //合并summary单元格
        p0.GetRow(0).MergeCells(0, 5);
        //合并url单元格
        urlRow.MergeCells(1, 5);
        //合并描述单元格
        descriptionRow.MergeCells(1, 5);
        //合并请求单元格
        requestMethodRow.MergeCells(1, 5);
        //合并请求type单元格
        requestTypeRow.MergeCells(1, 5);
        //        return 
        //$@"<table>
        //    {info}
        //    {paramsResult}
        //{requestBodyPropertiesResult}
        //    {requestBody}
        // {responses}
        //    {""}
        //    {examples}
        //</table>";
        //return string.Empty;
    }
}