using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;

namespace WindowsFormsApplication1.DocumentLayoutHelper {
    public static class TextBoxLayoutHelper {

        // get information about a current text box layout
        public static string GetInformationAboutCurrentTextBox(SubDocument subDocument, DocumentLayout currentLayout, DocumentPosition docPosition) {
            string returnedInformation = "!A TEXTBOX CONTENT IS SELECTED!\r\n";

            LayoutIterator layoutIterator = new LayoutIterator(currentLayout, subDocument.Range);
            LayoutPage currentTextBoxPage = null;
            LayoutTextBox currentTextBox = null;

            while(layoutIterator.MoveNext()) {
                LayoutElement element = layoutIterator.Current;
                if(element is LayoutTextBox) {
                    currentTextBox = (element as LayoutTextBox);
                    if (currentTextBox.Parent is LayoutPage) currentTextBoxPage = currentTextBox.Parent as LayoutPage;
                }
                if(element is PlainTextBox) {
                    PlainTextBox currentPlaintTextBox = element as PlainTextBox;
                    if(currentPlaintTextBox.Range.Contains(docPosition.ToInt())) {
                        returnedInformation += String.Format("Selected content: {0}\r\n", currentPlaintTextBox.Text);

                        LayoutRow currentRow = currentPlaintTextBox.Parent as LayoutRow;
                        int currentLineIndex = currentTextBox.Rows.ToList().IndexOf(currentRow);
                        returnedInformation += String.Format("Line index: {0}\r\n", currentLineIndex + 1);
                        returnedInformation += String.Format("Selected block bounds: {0}\r\n", currentPlaintTextBox.Bounds);
                        break;
                    }
                }
            }

            returnedInformation += String.Format("TEXTBOX bounds: {0}\r\n", currentTextBox.Bounds);
            returnedInformation += String.Format("\r\n!!Content information:\r\n");
            returnedInformation += GetInformationAboutCurrentTextBoxContent(currentTextBox);

            if(currentTextBoxPage != null) {
                returnedInformation += PageLayoutHelper.GetInformationAboutCurrentPage(currentLayout, currentTextBoxPage, docPosition);
            }
            return returnedInformation;
        }

        public static string GetInformationAboutCurrentTextBoxContent(LayoutTextBox currentTextBox) {
            string returnedInformation = "";
            CustomDocumentLayoutVisitor visitor = new CustomDocumentLayoutVisitor();
            visitor.Visit(currentTextBox);
            returnedInformation += String.Format("Lines count: {0}\r\n", visitor.TextLinesCount);
            returnedInformation += String.Format("Words count: {0}\r\n", visitor.WordsCount);
            returnedInformation += String.Format("Plain text: {0}\r\n", currentTextBox.Document.GetText(currentTextBox.Document.Range));
            return returnedInformation;
        }
    }
}
