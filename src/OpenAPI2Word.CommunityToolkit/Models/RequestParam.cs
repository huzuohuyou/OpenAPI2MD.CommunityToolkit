namespace OpenAPI2Word.CommunityToolkit.Models;

public class RequestParam
{
    
    public string? PropertyName { get; set; }
    public string? PropertyType { get; set; }
    public string? ParamType { get; set; } = "Body";
    public string? IsRequired { get; set; }
    public string IsRequiredDisplay => Equals("True", IsRequired) ? "Y" : "N";
    public string? Description { get; set; }
    
    public string? Example { get; set; }
    public void Generate(XWPFTable table)
    {
        var propertyRow = table.CreateRow();
        propertyRow.GetCell(0).SetText(PropertyName??"");
        propertyRow.GetCell(1).SetText(PropertyType ?? "");
        propertyRow.GetCell(2).SetText(ParamType ?? "");
        propertyRow.GetCell(3).SetText(IsRequiredDisplay ?? "");
        propertyRow.GetCell(4).SetText(Description ?? "");
        propertyRow.GetCell(5).SetText(Example ?? "");
    }
}