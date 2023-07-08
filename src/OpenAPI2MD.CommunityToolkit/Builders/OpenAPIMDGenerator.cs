using OpenApi2Doc.CommunityToolkit.Builders;

namespace OpenApi2Md.CommunityToolkit.Builders;

public class OpenApiMdGenerator : DocumentBuilder<StringBuilder>
{


    public override void Reset()
    {
        doc.Clear();
    }

    protected override void InitDoc()
    {
        doc = new StringBuilder();
    }

    protected override void BuildInfo()
    {

        doc.Append($"{ApiDocument.Info.Description} \n");
        doc.Append($"{ApiDocument.Info?.Contact?.Name} \n");
        doc.Append($"{ApiDocument.Info?.Contact?.Email} \n");
    }

    protected override void BeginBuildPathItem()
    {
        doc.Append($@"<table>");
    }

    protected override void AfterBuildPathItem()
    {
        doc.Append($@"</table>");
    }

    protected override void BuildTag()
    {
        if (!CurrentOperation.Tags.FirstOrDefault().Name.Equals(CurrentPathTag))
        {
            CurrentPathTag = CurrentOperation?.Tags.FirstOrDefault()?.Name;
            doc.Append($" \n## {CurrentPathTag} \n");
        }
    }

    protected override void BuildSummary()
    {
        if (Equals("获取报警/事件历史数据【此方法以后不在维护请使用alarms/actions/get-history】",CurrentOperation.Summary))
        {
            
        }
        doc.Append($@"

### {CurrentOperation.Summary}
");
    }

    protected override void BuildOperationId()
    {
        doc.Append($@"
<tr bgcolor=""{MdColor.bgcolor}"">
<td >OperationId</td>
<td colspan=""5"" >{CurrentOperation.OperationId}</td>
</tr>
");
    }

    protected override void BuildDescription()
    {
        doc.Append($@"
<tr>
<td >接口描述</td>
<td colspan=""5""  >{CurrentOperation.Description}</td>
</tr>");
    }

    protected override void BuildRequestMethod()
    {
        doc.Append($@"
<tr>
<td >请求方式</td>
<td colspan=""5""  >{CurrentPathItem.Operations.Keys.FirstOrDefault().ToString()}</td>
</tr>");
    }

    protected override void BuildRequestType()
    {
        doc.Append($@"");
    }

    protected override void BuildRequestParams()
    {
        doc.Append($@"
<tr>
<td>参数名</td>
<td>数据类型</td>
<td>参数类型</td>
<td>必需</td>
<td>描述</td>
<td>示例</td>
</tr>");
        CurrentOperation?.Parameters.ToList().ForEach(p =>
        {
            var va = string.Empty;
            if (p.Schema.Type == "array" && p.Example != null)
            {
                dynamic d = p.Example;
                var v = d[0].Value;
                va = v.ToString();
            }
            else if (p.Example != null)
            {
                dynamic d = p.Example;
                var v = d.Value;
                va = v.ToString();
            }

            var r = p.Required ? "Y" : "N";
            doc.Append($@"<tr>
<td >{p.Name}</td>
<td >{p.Schema.Type}</td>
<td >{p.In.ToString()}</td>
<td >{r}</td>
<td >{p.Description}</td>
<td >{va}</td>
</tr>");

        });

    }

    protected override void BuildRequestBodies()
    {
        var requestBodys = new List<RequestBody>();
        var c = new RequestProperiesGenerator().Excute(CurrentOperation.RequestBody?.Content?.FirstOrDefault()
        .Value?.Schema);
        if (!Equals(null, c))
            requestBodys.AddRange(c);
        if (requestBodys.Count == 0)
            return;
        doc.Append($@"
<tr>
<td bgcolor=""{MdColor.bgcolor}"">参数名</td>
<td bgcolor=""{MdColor.bgcolor}"">数据类型</td>
<td bgcolor=""{MdColor.bgcolor}"">参数类型</td>
<td bgcolor=""{MdColor.bgcolor}"">必需</td>
<td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">描述</td>
</tr>");
        requestBodys.ForEach(r =>
        {
            doc.Append($@"
<tr>
<td >{r.PropertyName}</td>
<td >{r.PropertyType}</td>
<td >{r.ParamType}</td>
<td >{r.IsRequired}</td>
<td colspan=""2"">{r.Description}</td>
</tr>"
    );
        });
        doc.Append($@"
<tr>
<td colspan=""6""  >示例</td>
</tr>
<tr>
<tr>
<td colspan=""6""  >

{new ExampleValueGenerator().Excute(CurrentOperation.RequestBody?.Content?.FirstOrDefault().Value?.Schema)}

</td>
</tr>");
    }

    protected override void BuildRequestBodyExample()
    {
        return;
    }

    protected override void BuildResponse()
    {
        var type = new StringBuilder();
        if (CurrentResponse.Content.Count > 0)
        {
            type = type.Append(CurrentResponse.Content.Count > 0 ? CurrentResponse.Content.FirstOrDefault().Value.Schema.Type : default);
            if (Equals("array", type.ToString()))
                type.Append($@":{CurrentResponse.Content.FirstOrDefault().Value.Schema?.Items?.Reference?.Id}");
            if (Equals("object", type.ToString()))
                type.Append($@":{CurrentResponse.Content.FirstOrDefault().Value.Schema?.Reference?.Id}");
        }

        doc.Append($@"
<tr>
<td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">状态码</td>
<td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">描述</td>
<td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">类型</td>
</tr>");

        doc.Append($@"<tr>
<td colspan=""2"">{CurrentResponseCode}</td>
<td colspan=""2"">{CurrentResponse.Description}</td>
<td colspan=""2"" >{CurrentResponse.Content.FirstOrDefault().Key}</td>
</tr>");
    }

    protected override void BuildResponseFields()
    {
        doc.Append($@"
<tr>
<td bgcolor=""{MdColor.bgcolor}"">返回属性名</td>
<td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">数据类型</td>
<td colspan=""3"" bgcolor=""{MdColor.bgcolor}"">说明</td>
</tr>");
        var c = new ResponseProperiesGenerator().Excute(CurrentResponse.Content.FirstOrDefault().Value?.Schema);
        foreach (var s in c)
        {
            doc.Append($@"
<tr>
<td >{s.PropertyName}</td>
<td colspan=""2"">{s.PropertyType}</td>
<td colspan=""3"" >{s.Description}</td>
</tr>");
        }

    }

    protected override void BuildResponseExample()
    {
        var example = new ExampleValueGenerator().Excute(CurrentResponse.Content.Count > 0 ? CurrentResponse.Content.FirstOrDefault().Value.Schema : default);
        doc.Append($@"
<tr>
<td >示例</td>
<td colspan=""6""  >

{example}

</td>
</tr>");
    }




    protected override void BuildTitle()
    {
        doc.Append($" # {ApiDocument.Info.Title}({ApiDocument.Info.Version}) \n");
    }

    protected override void BuildToc()
    {
        doc.Append($"<!-- @import \"[TOC]\" {{cmd=\"toc\" depthFrom=2 depthTo=3 orderedList=false}} -->\n");
    }

    protected override StringBuilder OutputDoc(string savePath = "")
    {
        var s = doc.ToString();
        File.WriteAllText(Path.Combine(savePath, "swagger.md"), s);
        if (File.Exists(Path.Combine(savePath, "swagger.md")))
        {
            return doc;
        }
        return new StringBuilder();
    }


}