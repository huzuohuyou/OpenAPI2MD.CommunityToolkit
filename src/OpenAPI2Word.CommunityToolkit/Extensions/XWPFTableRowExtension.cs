namespace OpenAPI2Word.CommunityToolkit.Extensions
{
    public static class XWPFTableRowExtension
    {
        public static void SetColor(this XWPFTableRow row, string rgbStr)
        {
            row.GetTableCells().ForEach(c =>
            {
                c.SetColor(rgbStr);
            });
        }
    }
}
