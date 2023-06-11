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

        public static void SetBorderBottom(this XWPFTableRow row)
        {
            row.GetTableCells().ForEach(c =>
            {
                c.SetBorderBottom(XWPFTable.XWPFBorderType.NONE,0,0,"#ffffff");
            });

        }
    }
}
