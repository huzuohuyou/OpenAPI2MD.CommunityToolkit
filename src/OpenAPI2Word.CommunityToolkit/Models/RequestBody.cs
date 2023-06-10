namespace OpenAPI2Word.CommunityToolkit.Models;

public class RequestBody
{
    public string? PropertyName { get; set; }
    public string? PropertyType { get; set; }
    public string? ParamType { get; set; } = "Body";
    public string? IsRequired { get; set; }
    public string? Description { get; set; }

    public void Generate(XWPFTable table)
    {
        var propertyRow = table.CreateRow();
        propertyRow.GetCell(0).SetText(PropertyName ?? "");
        propertyRow.GetCell(1).SetText(PropertyType ?? "");
        propertyRow.GetCell(2).SetText(ParamType ?? "");
        propertyRow.GetCell(3).SetText(IsRequired ?? "");
        propertyRow.GetCell(4).SetText(Description ?? "");
        propertyRow.MergeCells(4,5);
    }
}