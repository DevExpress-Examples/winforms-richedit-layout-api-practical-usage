Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native
Imports WindowsFormsApplication1.DocumentLayoutHelper

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits RibbonForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			richEditControl1.LoadDocument("testDocument.docx")
		End Sub

		Private Sub barButtonItem1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barButtonItem1.ItemClick
			memoEdit1.Text = RichEditDocumentLayoutAnalyzer.GetInformationAboutRichEditDocumentLayout(richEditControl1.Document, richEditControl1.DocumentLayout)
		End Sub
	End Class
End Namespace
