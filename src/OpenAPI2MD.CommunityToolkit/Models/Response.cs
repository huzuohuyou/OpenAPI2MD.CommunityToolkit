using Microsoft.OpenApi.Models;
using OpenAPI2MD.CommunityToolkit.Generators;

namespace OpenAPI2MD.CommunityToolkit.Models;

public class Response
{
    public string Code { get; set; }
    public string Des { get; set; }
    public string Remark { get; set; }
    public string ResponseType { get; set; }
    public string ResponseDataType { get; set; }
    public List<Schema> Schemas { get; set; } = new();
    public OpenApiSchema OpenApiSchema { get; set; }
    public override string ToString()
    {
        var fields = new StringBuilder(
$@"<tr>
    <td bgcolor=""{MdColor.bgcolor}"">返回属性名</td>
    <td colspan=""2"" bgcolor=""{MdColor.bgcolor}"">数据类型</td>
    <td colspan=""3"" bgcolor=""{MdColor.bgcolor}"">说明</td>
    
</tr>");
        Schemas.ForEach(r =>
        {
            fields.Append(r.ToString());
        });

        var responseExample = new StringBuilder(
$@"<tr>
    <td colspan=""6"" bgcolor=""{MdColor.bgcolor}"">示例</td>
</tr>
<tr>
<td colspan=""6"">

{new ExampleValueGenerator().Excute(OpenApiSchema)}
</td>
</tr>
");
        return 
$@"<tr>
    <td >{Code}</td>
    <td >{Des}</td>
    <td colspan=""2"" >{ResponseType}</td>
    <td colspan=""2"" >{ResponseDataType}</td>
</tr>
{fields.ToString()}


{responseExample.ToString().Trim()}


";
    }
}