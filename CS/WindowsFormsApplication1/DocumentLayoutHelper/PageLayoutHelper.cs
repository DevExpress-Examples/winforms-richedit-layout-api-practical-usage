using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;

namespace WindowsFormsApplication1.DocumentLayoutHelper {
    public static class PageLayoutHelper {
        public static string GetInformationAboutCurrentPage(DocumentLayout currentDocumentLayout, LayoutPage currentPage, DocumentPosition currentPosition) {
            string returnedInformation = "";

            int currentPageIndex = currentDocumentLayout.GetPageIndex(currentPage);
            int totalPageCount = currentDocumentLayout.GetFormattedPageCount();

            // get information about page content using a LayoutVisitor descendant
            CustomDocumentLayoutVisitor visitor = new CustomDocumentLayoutVisitor();
            visitor.Visit(currentPage);

            // get information about page bounds using PageArea properties
            PageAreaProperties pageAreaProperties = CaculatePageAreaProperties(currentPage.PageAreas[0], currentPosition);

            returnedInformation += String.Format("\r\nPage: {0} of {1}\r\n", currentPageIndex + 1, totalPageCount);
            returnedInformation += String.Format("Current page range: {0} - {1}\r\n", currentPage.MainContentRange.Start, currentPage.MainContentRange.Start + currentPage.MainContentRange.Length - 1);
            returnedInformation += String.Format("Images count: {0}\r\n", visitor.ImagesCount);
            returnedInformation += String.Format("Tables count: {0}\r\n", visitor.TablesCount);
            returnedInformation += String.Format("Text Boxes count: {0}\r\n", visitor.TextBoxesCount);
            returnedInformation += String.Format("Paragraphs count: {0}\r\n", visitor.ParagraphsCount);
            returnedInformation += String.Format("Text lines count: {0}\r\n", visitor.TextLinesCount);
            returnedInformation += String.Format("Words (text blocks) count: {0}\r\n", visitor.WordsCount);

            returnedInformation += String.Format("\r\nColumn: {0} of {1}\r\n", pageAreaProperties.currentColumnIndex, pageAreaProperties.columnCount);
            returnedInformation += String.Format("Current COLUMN content bounds: {0}\r\n", pageAreaProperties.currentColumnBounds);
            returnedInformation += String.Format("Current PAGE content bounds: {0}\r\n", pageAreaProperties.currentPageBounds);

            return returnedInformation;
        }

        public static PageAreaProperties CaculatePageAreaProperties(LayoutPageArea pageArea, DocumentPosition pos) {
            PageAreaProperties pageAreaProperties = new PageAreaProperties();
            pageAreaProperties.columnCount = pageArea.Columns.Count;

            pageAreaProperties.currentPageBounds.Location = pageArea.Columns[0].Bounds.Location;

            for(int i = 0; i < pageArea.Columns.Count; i++) {
                int currentColumnContentHeight = pageArea.Columns[i].Rows.Last.Bounds.Bottom - pageArea.Columns[i].Rows.First.Bounds.Top;
                if(pageArea.Columns[i].Range.Contains(pos.ToInt())) {
                    pageAreaProperties.currentColumnIndex = i;
                    pageAreaProperties.currentColumnBounds.Location = pageArea.Columns[i].Bounds.Location;
                    pageAreaProperties.currentColumnBounds.Width = pageArea.Columns[i].Bounds.Width;
                    pageAreaProperties.currentColumnBounds.Height = currentColumnContentHeight;
                }
                pageAreaProperties.currentPageBounds.Width += pageArea.Columns[i].Bounds.Width;
                pageAreaProperties.currentPageBounds.Height = Math.Max(pageAreaProperties.currentPageBounds.Height, currentColumnContentHeight);
            }
            return pageAreaProperties;
        }
    }

    public class PageAreaProperties {
        public int currentColumnIndex = 0;
        public int columnCount = 0;
        public Rectangle currentColumnBounds = Rectangle.Empty;
        public Rectangle currentPageBounds = Rectangle.Empty;
    }
}
