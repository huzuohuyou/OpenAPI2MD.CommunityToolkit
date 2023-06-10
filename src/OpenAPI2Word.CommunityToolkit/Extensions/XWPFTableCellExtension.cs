namespace OpenAPI2Word.CommunityToolkit.Extensions
{
    internal static class XWPFTableCellExtension
    {
        public static void SetText2(this XWPFTableCell cell, string content)
        {
            var contents = content.Split("<br>");
            if (contents.Length <= 1)
            {
                contents = content.Split("\r\n");
            }

            var index = 0;
            foreach (var s in contents)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                XWPFParagraph p1;
                if (index == 0)
                {
                    p1 = cell.Paragraphs[0];
                }
                else
                {
                    p1 = cell.AddParagraph();
                }
                XWPFRun r1 = p1.CreateRun();
                r1.FontFamily = "microsoft yahei";
                r1.FontSize = 10;
                r1.IsBold = false;
                r1.SetText(s);
                index++;
            }
        }

        public static void SetText3(this XWPFTableCell cell, string content)
        {
            XWPFParagraph p1 = cell.Paragraphs[0];
            XWPFRun r1 = p1.CreateRun();
            r1.FontFamily = "microsoft yahei";
            r1.FontSize = 10;
            r1.IsBold = false;
            r1.SetText(content);
        }
    }
}

