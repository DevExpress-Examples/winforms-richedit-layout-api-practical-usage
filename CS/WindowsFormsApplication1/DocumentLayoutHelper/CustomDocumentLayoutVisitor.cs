using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Layout;

namespace WindowsFormsApplication1.DocumentLayoutHelper {
    public class CustomDocumentLayoutVisitor : DevExpress.XtraRichEdit.API.Layout.LayoutVisitor {
        public int ParagraphsCount = 0;
        public int TextLinesCount = 0;
        public int WordsCount = 0;
        public int TablesCount = 0;
        public int TextBoxesCount = 0;
        public int ImagesCount = 0;

        protected override void VisitTable(LayoutTable table) {
            TablesCount++;
            base.VisitTable(table);
        }

        protected override void VisitTextBox(LayoutTextBox textBox) {
            TextBoxesCount++;
            base.VisitTextBox(textBox);
        }

        protected override void VisitFloatingObjectAnchorBox(FloatingObjectAnchorBox floatingObjectAnchorBox) {
            LayoutType objectType = floatingObjectAnchorBox.FloatingObjectBox.Type;
            if(objectType == LayoutType.FloatingPicture) ImagesCount++;
            base.VisitFloatingObjectAnchorBox(floatingObjectAnchorBox);
        }

        protected override void VisitInlinePictureBox(InlinePictureBox inlinePictureBox) {
            ImagesCount++;
            base.VisitInlinePictureBox(inlinePictureBox);
        }

        protected override void VisitPlainTextBox(PlainTextBox plainTextBox) {
            WordsCount++;
            base.VisitPlainTextBox(plainTextBox);
        }

        protected override void VisitParagraphMarkBox(PlainTextBox paragraphMarkBox) {
            ParagraphsCount++;
            base.VisitParagraphMarkBox(paragraphMarkBox);
        }

        protected override void VisitRow(LayoutRow row) {
            TextLinesCount++;
            base.VisitRow(row);
        }
    }
}
