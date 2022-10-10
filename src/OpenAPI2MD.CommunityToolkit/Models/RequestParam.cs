namespace OpenAPI2MD.CommunityToolkit.Models;

public class RequestParam
{
    public string Name { get; set; }
    public string DataType { get; set; }
    public string ParamType { get; set; }
    public string IsRequired { get; set; }
    public string Des { get; set; }
    public string Example { get; set; }
    public override string ToString()
    {
        return 
$@"<tr>
    <td >{Name}</td>
    <td >{DataType}</td>
    <td >{ParamType}</td>
    <td >{IsRequired}</td>
    <td >{Des}</td>
    <td >{Example}</td>
</tr>";
    }
}