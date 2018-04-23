using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;

namespace WindowsFormsApplication1.DocumentLayoutHelper {
    public static class RichEditDocumentLayoutAnalyzer {
        public static string GetInformationAboutRichEditDocumentLayout(Document currentDocument, DocumentLayout currentDocumentLayout) {
            SubDocument subDocument = currentDocument.CaretPosition.BeginUpdateDocument();
            DocumentPosition docPosition = subDocument.CreatePosition(currentDocument.CaretPosition.ToInt() == 0 ? 0 : currentDocument.CaretPosition.ToInt() - 1);

            ReadOnlyShapeCollection shapes = subDocument.Shapes.Get(subDocument.CreateRange(docPosition, 1));
            ReadOnlyDocumentImageCollection images = subDocument.Images.Get(subDocument.CreateRange(docPosition, 1));

            if(shapes.Count == 0 && images.Count == 0) docPosition = subDocument.CreatePosition(currentDocument.CaretPosition.ToInt());

            string returnedInformation = "";
            // get infromation about a current document element
            returnedInformation += GetInformationAboutCurrentDocumentElement(currentDocument, currentDocumentLayout, subDocument, docPosition);

            // collect information about CURRENT PAGE
            RangedLayoutElement layoutPosition = currentDocumentLayout.GetElement<RangedLayoutElement>(docPosition);
            if(layoutPosition != null) {
                int currentPageIndex = currentDocumentLayout.GetPageIndex(layoutPosition);
                returnedInformation += PageLayoutHelper.GetInformationAboutCurrentPage(currentDocumentLayout, currentDocumentLayout.GetPage(currentPageIndex), docPosition);                
            }

            currentDocument.CaretPosition.EndUpdateDocument(subDocument);

            return returnedInformation;
        }

        public static string GetInformationAboutCurrentDocumentElement(Document currentDocument, DocumentLayout currentDocumentLayout, SubDocument currentSubDocument, DocumentPosition docPosition) {
            if(currentSubDocument.GetSubDocumentType() == SubDocumentType.TextBox) {
                return TextBoxLayoutHelper.GetInformationAboutCurrentTextBox(currentSubDocument, currentDocumentLayout, docPosition);
            }
            if(currentSubDocument.GetSubDocumentType() == SubDocumentType.Main) {

                RangedLayoutElement tableCell = currentDocumentLayout.GetElement(docPosition, LayoutType.TableCell);
                if(tableCell != null)
                    // collect information about TABLE CELL
                    return DocumentElementLayoutHelper.GetInformationAboutCurrentTableCell(currentDocument, tableCell as LayoutTableCell);

                RangedLayoutElement imageinline = currentDocumentLayout.GetElement(docPosition, LayoutType.InlinePictureBox);
                if(imageinline != null)
                    // collect information about INLINE PICTURE
                    return DocumentElementLayoutHelper.GetInformationAboutCurrentInlinePicture(imageinline as InlinePictureBox);


                RangedLayoutElement floatingObjectAnchor = currentDocumentLayout.GetElement(docPosition, LayoutType.FloatingObjectAnchorBox);
                if(floatingObjectAnchor != null)
                    // collect information about FLOATING OBJECT
                    return DocumentElementLayoutHelper.GetInformationAboutCurrentFloatingObject(floatingObjectAnchor as FloatingObjectAnchorBox, currentDocumentLayout);

                RangedLayoutElement regularTextBlock = currentDocumentLayout.GetElement(docPosition, LayoutType.PlainTextBox);
                if(regularTextBlock != null)
                    // collect information about REGULAR TEXT BLOCK
                    return DocumentElementLayoutHelper.GetInformationAboutRegularTextBlock(regularTextBlock as PlainTextBox, currentDocument);
            }
            return "";
        }
    }
}
