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

        var p0 = doc.CreateTable(1, 6);//默认有一行
        
        //总结
        var summaryCell = p0.GetRow(0).GetTableCells().FirstOrDefault();
        p0.GetRow(0).SetColor("#696969");
        summaryCell.SetTableHeaderText(Summary);
        //设置列宽

        //名称
        var nameRow = p0.CreateRow();
        nameRow.GetCell(0).SetText3("接口名称");
        nameRow.GetCell(1).SetText3(Name);
        nameRow.MergeCells(1, 5);
        //描述
        var descriptionRow = p0.CreateRow();
        descriptionRow.GetCell(0).SetText3("接口描述");
        descriptionRow.GetCell(1).SetDescription(Description ?? "");
        //URL
        var urlRow = p0.CreateRow();
        urlRow.GetCell(0).SetText3("URL");
        urlRow.GetCell(1).SetText3(Url ?? "");
        //请求方式
        var requestMethodRow = p0.CreateRow();
        requestMethodRow.GetCell(0).SetText3("请求方式");
        requestMethodRow.GetCell(1).SetText3(RequestMethod ?? "");
        //请求类型
        var requestTypeRow = p0.CreateRow();
        requestTypeRow.GetCell(0).SetText3("请求类型");
        requestTypeRow.GetCell(1).SetText3(ResponseType ?? "");
        //HEADER \QUERY 参数头
        var requestParamPropertiesRow = p0.CreateRow();
        requestParamPropertiesRow.SetColor("#696969");
        requestParamPropertiesRow.GetCell(0).SetTableColumnWidth(1500).SetTableHeaderText("参数名  ");
        requestParamPropertiesRow.GetCell(1).SetTableColumnWidth(1500).SetTableHeaderText("数据类型");
        requestParamPropertiesRow.GetCell(2).SetTableColumnWidth(1500).SetTableHeaderText("参数类型");
        requestParamPropertiesRow.GetCell(3).SetTableColumnWidth(1500).SetTableHeaderText("必填");
        requestParamPropertiesRow.GetCell(4).SetTableColumnWidth(1500).SetTableHeaderText("说明    ");
        requestParamPropertiesRow.GetCell(5).SetTableColumnWidth(1500).SetTableHeaderText("示例    ");

        var pIndex = 0;
        RequestParams.ForEach(r =>
        {
            r.Generate(p0, pIndex);
            pIndex++;
        });

        //BODY 参数头
        var requestBodyPropertiesRow = p0.CreateRow();
        requestBodyPropertiesRow.GetCell(0).SetTableHeaderText("参数名");
        requestBodyPropertiesRow.GetCell(1).SetTableHeaderText("数据类型");
        requestBodyPropertiesRow.GetCell(2).SetTableHeaderText("参数类型");
        requestBodyPropertiesRow.GetCell(3).SetTableHeaderText("必填");
        requestBodyPropertiesRow.GetCell(4).SetTableHeaderText("说明");
        requestBodyPropertiesRow.MergeCells(4, 5);
        var reindex = 0;
        RequestBodys.ForEach(r =>
        {
            r.Generate(p0,reindex);
            reindex++;
        });

        //BODY 参数头
        var requestBodyExampleRow = p0.CreateRow();
        requestBodyExampleRow.GetCell(0).SetText3("示例");
        requestBodyExampleRow.MergeCells(0, 5);
        var requestBody = new ExampleValueGenerator().Excute(RequestBody);
        //BODY Json
        var requestBodyExampleJsonRow = p0.CreateRow();
        requestBodyExampleJsonRow.GetCell(0).SetJsonOrBrText(requestBody ?? "");
        requestBodyExampleJsonRow.MergeCells(0, 5);
        var rindex = 0;
        Responses.ForEach(r =>
        {
            //BODY 参数头
            var responseRow = p0.CreateRow();
            //responseRow.SetColor("#696969");
            responseRow.GetCell(0).SetTableHeaderText("状态码");
            responseRow.GetCell(1).SetTableHeaderText("描述");
            responseRow.GetCell(2).SetTableHeaderText("类型");
            responseRow.GetCell(3).SetTableHeaderText("数据类型");
            responseRow.MergeCells(3, 5);
            r.Generate(p0);
           
            rindex++;
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
        //设置内部边框
        p0.SetInsideHBorder(XWPFTable.XWPFBorderType.NONE,0,0,"#ffffff");
        p0.SetInsideVBorder(XWPFTable.XWPFBorderType.NONE, 0, 0, "#ffffff");
    
    }
}