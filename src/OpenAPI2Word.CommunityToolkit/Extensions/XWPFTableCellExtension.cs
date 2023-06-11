

namespace OpenAPI2Word.CommunityToolkit.Extensions
{
    internal static class XWPFTableCellExtension
    {
        public static void SetJsonOrBrText(this XWPFTableCell cell, string content)
        {
            cell.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
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
                p1.FirstLineIndent = 99;
                XWPFRun r1 = p1.CreateRun();
                r1.FontFamily = "microsoft yahei";
                r1.FontSize = 10;
                r1.IsBold = false;
                p1.IndentFromLeft = 100 * (s.LastIndexOf('·') + 1);
                r1.SetText(s.Contains('·') ? s.Replace("·", "") : s);
                index++;
            }
        }

        public static void SetText3(this XWPFTableCell cell, string content)
        {
            cell.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
            XWPFParagraph p1 = cell.Paragraphs[0];
            XWPFRun r1 = p1.CreateRun();
            p1.VerticalAlignment = TextAlignment.CENTER;
            p1.FirstLineIndent = 99;
            r1.FontFamily = "microsoft yahei";
            r1.FontSize = 10;
            r1.SetText(content);
        }

        public static void SetColumCellText(this XWPFTableCell cell, string content, int length = 12)
        {
            cell.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
            for (int i = 0; i < content.Length; i += length)
            {
                XWPFParagraph p1;
                if (i == 0)
                    p1 = cell.Paragraphs[0];
                else
                    p1 = cell.AddParagraph();

                XWPFRun r1 = p1.CreateRun();
                p1.VerticalAlignment = TextAlignment.CENTER;
                p1.FirstLineIndent = 99;
                r1.FontFamily = "microsoft yahei";
                r1.FontSize = 10;
                p1.IndentFromLeft = 100 * (content.LastIndexOf('·') + 1);
                var s = content.Contains('·') ? content.Replace("·", "") : content;
                r1.SetText(new string(s.Skip(i).Take(length).ToArray()));
            }

        }



        public static void SetTableHeaderText(this XWPFTableCell cell, string content)
        {
            cell.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
            cell.GetTableRow().SetColor("696969");
            XWPFParagraph p1 = cell.Paragraphs[0];
            p1.SpacingBeforeLines = 20;
            p1.SpacingAfterLines = 20;
            p1.FirstLineIndent = 99;
            p1.VerticalAlignment = TextAlignment.CENTER;
            XWPFRun r1 = p1.CreateRun();
            r1.FontFamily = "microsoft yahei";
            r1.FontSize = 10;
            r1.IsBold = true;
            r1.SetColor("#ffffff");
            r1.SetText(content);
        }

        public static XWPFTableCell SetTableColumnWidth(this XWPFTableCell cell, int width)
        {
            //不好使
            //var tcpr = cell.GetCTTc().AddNewTcPr();

            //var cellw = tcpr.AddNewTcW();
            //cellw.type = ST_TblWidth.dxa;
            //cellw.w = width.ToString();

            //不好使
            CT_TcPr m_Pr = cell.GetCTTc().AddNewTcPr();
            m_Pr.tcW = new CT_TblWidth();
            m_Pr.tcW.w = width.ToString();
            m_Pr.tcW.type = ST_TblWidth.dxa;
            return cell;
        }
    }
}

