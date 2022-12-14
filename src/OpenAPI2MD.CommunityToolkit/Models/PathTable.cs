using Microsoft.OpenApi.Models;
using OpenAPI2MD.CommunityToolkit.Generators;

namespace OpenAPI2MD.CommunityToolkit.Models;

public class PathTable
{
    public string? Summary { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? RequestMethod { get; set; }
    public string? RequestType { get; set; }
    public string? ResponseType { get; set; }
    public List<RequestParam> RequestParams { get; set; }=new ();
    public List<RequestBody> RequestBodys { get; set; } = new();
    public OpenApiSchema? RequestBody { get; set; }
    public List<Response> Responses { get; set; }= new ();
    public List<Example> Examples { get; set; } = new ();
    
    public override string ToString()
    {
        var info = $@"
<tr>
    <td colspan=""6"" bgcolor=""{MdColor.bgcolor}"">{Summary}</td>
</tr>
<tr>
    <td >接口名称</td>
    <td colspan=""5"">{Name}</td>
</tr>
<tr>
    <td >接口描述</td>
    <td colspan=""5"">{Description}</td>
</tr>
<tr>
    <td >URL</td>
    <td colspan=""5"">{Url}</td>
</tr>
<tr>
    <td >请求方式</td>
    <td colspan=""5"">{RequestMethod}</td>
</tr>
<tr>
    <td >请求类型</td>
    <td colspan=""5"">{RequestType}</td>
</tr>";
//        < tr >
//    < td > 返回类型 </ td >
//    < td colspan = ""4"" >{ ResponseType}</ td >
//</ tr >
        var paramHeader = new StringBuilder(
$@"<tr>
    <td bgcolor=""{MdColor.bgcolor}"">参数名</td>
    <td bgcolor=""{MdColor.bgcolor}"">数据类型</td>
    <td bgcolor=""{MdColor.bgcolor}"">参数类型</td>
    <td bgcolor=""{MdColor.bgcolor}"">是否必填</td>
    <td bgcolor=""{MdColor.bgcolor}"">说明</td>
    <td bgcolor=""{MdColor.bgcolor}"">示例</td>
</tr>");
        RequestParams.ForEach(r =>
        {
            paramHeader.Append(r);
        });

        var paramsResult = string.Empty;
        if (!Equals(RequestParams.Count, 0))
            paramsResult= paramHeader.ToString();

        var requestBodyProperties = new StringBuilder(
            $@"<tr>
    <td bgcolor=""{MdColor.bgcolor}"">参数名</td>
    <td bgcolor=""{MdColor.bgcolor}"">数据类型</td>
    <td bgcolor=""{MdColor.bgcolor}"">参数类型</td>
    <td bgcolor=""{MdColor.bgcolor}"">是否必填</td>
    <td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">说明</td>
</tr>");
        RequestBodys.ForEach(r =>
        {
            requestBodyProperties.Append(r);
        });

        var requestBodyPropertiesResult = string.Empty;
        if (!Equals(RequestBodys.Count, 0))
            requestBodyPropertiesResult = requestBodyProperties.ToString();


        var responses = new StringBuilder();
        Responses.ForEach(r =>
        {
            responses.Append(
$@"<tr>
    <td bgcolor=""{MdColor.bgcolor}"">状态码</td>
    <td colspan=""1"" bgcolor=""{MdColor.bgcolor}"">描述</td>
    <td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">类型</td>
    <td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">数据类型</td>
</tr>
{r.ToString()}");
        });
        var examples = $@"";

        var requestBody = new ExampleValueGenerator().Excute(RequestBody);
        if (!string.IsNullOrWhiteSpace(requestBody))
        {
            requestBody = 
$@"<tr>
    <td colspan=""6"" bgcolor=""{MdColor.bgcolor}"">示例</td>
    </tr>
    <tr>
    <td colspan=""6"">

 {requestBody.Trim()}</td>
</tr>";
        }
        return 
$@"<table>
    {info}
    {paramsResult}
{requestBodyPropertiesResult}
    {requestBody}
 {responses}
    {""}
    {examples}
</table>";
    }
}