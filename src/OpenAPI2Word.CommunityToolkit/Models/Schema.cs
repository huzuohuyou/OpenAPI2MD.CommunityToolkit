namespace OpenAPI2Word.CommunityToolkit.Models;

public class Schema
{
    public string? PropertyName { get; set; }
    public string? PropertyType { get; set; }
    public string? Description { get; set; }
    public string? Example { get; set; }
    public void Generate(XWPFTable table)
    {
        var fieldsHeaderRow = table.CreateRow();
        fieldsHeaderRow.GetCell(0).SetText(PropertyName ?? "");
        fieldsHeaderRow.GetCell(1).SetText(PropertyType ?? "");
        fieldsHeaderRow.GetCell(2).SetText(Description ?? "");
        fieldsHeaderRow.MergeCells(2,5);
    }
}