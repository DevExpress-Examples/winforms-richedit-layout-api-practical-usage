using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;

namespace WindowsFormsApplication1.DocumentLayoutHelper {
    public static class DocumentElementLayoutHelper {
        
        // get information about a current regular text block
        public static string GetInformationAboutRegularTextBlock(PlainTextBox currentTextBlock, Document currentDocument) {
            string returnedInformation = "!A REGULAR TEXT BLOCK IS SELECTED!\r\n";
            returnedInformation += String.Format("Content: {0}\r\n", currentTextBlock.Text);

            LayoutRow currentRow = currentTextBlock.Parent as LayoutRow;
            LayoutColumn currentColumn = currentRow.Parent as LayoutColumn;
            int currentLineIndex = currentColumn.Rows.ToList().IndexOf(currentRow);
            returnedInformation += String.Format("Line index: {0}\r\n", currentLineIndex + 1);
            returnedInformation += String.Format("Start position: {0}\r\n", currentTextBlock.Range.Start);
            returnedInformation += String.Format("End position: {0}\r\n", currentTextBlock.Range.Start + currentTextBlock.Range.Length);

            int currentParagraphIndex = currentDocument.Paragraphs.ToList().IndexOf(currentDocument.Paragraphs.Get(currentDocument.CreatePosition(currentTextBlock.Range.Start)));
            returnedInformation += String.Format("Paragrpah index: {0}\r\n", currentParagraphIndex + 1);
            returnedInformation += String.Format("Text block bounds: {0}\r\n", currentTextBlock.Bounds);

            return returnedInformation;
        }

        // get information about a current floating object
        public static string GetInformationAboutCurrentFloatingObject(FloatingObjectAnchorBox currentObjectAnchor) {
            LayoutFloatingObject currentFloatingObject = currentObjectAnchor.FloatingObjectBox;
            string returnedInformation = "";
            if(currentFloatingObject.Type == LayoutType.FloatingPicture)
                returnedInformation = "!A FLOATING PICTURE IS SELECTED!\r\n";
            else if(currentFloatingObject.Type == LayoutType.TextBox)
                returnedInformation = "!A TEXT BOX IS SELECTED!\r\n";

            returnedInformation += String.Format("Anchor location: {0}\r\n", currentObjectAnchor.Bounds.Location);
            returnedInformation += String.Format("Object bounds: {0}\r\n", currentFloatingObject.Bounds);
            returnedInformation += String.Format("Rotation: {0}\r\n", currentFloatingObject.RotationAngle);

            if(currentFloatingObject.Type == LayoutType.TextBox) {
                LayoutTextBox currentTextBox = currentFloatingObject as LayoutTextBox;
                returnedInformation += String.Format("\r\n!!Content information:\r\n");
                returnedInformation += TextBoxLayoutHelper.GetInformationAboutCurrentTextBoxContent(currentTextBox);
            }
            else if(currentFloatingObject.Type == LayoutType.FloatingPicture) {
                LayoutFloatingPicture currentFloatingPicture = currentFloatingObject as LayoutFloatingPicture;
                returnedInformation += String.Format("\r\n!!Image properties:\r\n");
                returnedInformation += GetInformationAboutOfficeImage(currentFloatingPicture.Image);
            }

            return returnedInformation;
        }

        // get information about a current inline picture
        public static string GetInformationAboutCurrentInlinePicture(InlinePictureBox inlinePicture) {
            string returnedInformation = "!AN INLINE IMAGE IS SELECTED!\r\n";
            returnedInformation += String.Format("Start position: {0}\r\n", inlinePicture.Range.Start);
            returnedInformation += GetInformationAboutOfficeImage(inlinePicture.Image);
            return returnedInformation;
        }

        // get information about a current table cell
        public static string GetInformationAboutCurrentTableCell(Document currentDocument, LayoutTableCell tableCell) {
            string returnedInformation = "!A TABLE CELL IS SELECTED!\r\n";
            string cellText = currentDocument.GetText(currentDocument.CreateRange(tableCell.Range.Start, tableCell.Range.Length - 1));
            returnedInformation += String.Format("Cell's content: {0}\r\n", cellText);
            
            LayoutTableRow currentRow = tableCell.Parent as LayoutTableRow;
            if(currentRow == null) return returnedInformation;
            LayoutTable currentTable = currentRow.Parent as LayoutTable;
            if(currentTable == null) return returnedInformation;
            
            returnedInformation += String.Format("Row index: {0}\r\n", currentTable.TableRows.ToList().IndexOf(currentRow) + 1);
            for(int i = 0; i < currentRow.TableCells.Count; i++) {
                if(currentRow.TableCells[i] == tableCell) {
                    returnedInformation += String.Format("Column index: {0}\r\n", i + 1);
                    break;
                }
            }

            returnedInformation += String.Format("\r\n!Table information:\r\n");
            returnedInformation += String.Format("Row count: {0}\r\n", currentTable.TableRows.Count);
            int maxCellsCount = 0;
            for(int i = 0; i < currentTable.TableRows.Count; i++) {
                if(currentTable.TableRows[i].TableCells.Count > maxCellsCount) maxCellsCount = currentTable.TableRows[i].TableCells.Count;
            }
            returnedInformation += String.Format("Column count: {0}\r\n", maxCellsCount);
            return returnedInformation;
        }

        // get information about a current inline picture
        public static string GetInformationAboutOfficeImage(OfficeImage officeImage) {
            string returnedInformation = "";
            returnedInformation += String.Format("Format: {0}\r\n", officeImage.RawFormat);
            returnedInformation += String.Format("Original size: {0}\r\n", officeImage.SizeInPixels);
            return returnedInformation;
        }
    }
}
