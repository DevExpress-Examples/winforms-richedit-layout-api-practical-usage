using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;
using WindowsFormsApplication1.DocumentLayoutHelper;

namespace WindowsFormsApplication1 {
    public partial class Form1 : RibbonForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            richEditControl1.LoadDocument("testDocument.docx");
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            memoEdit1.Text = RichEditDocumentLayoutAnalyzer.GetInformationAboutRichEditDocumentLayout(richEditControl1.Document, richEditControl1.DocumentLayout);
        }
    }
}
