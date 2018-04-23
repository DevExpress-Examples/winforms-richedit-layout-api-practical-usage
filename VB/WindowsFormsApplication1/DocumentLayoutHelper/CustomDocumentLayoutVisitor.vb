Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraRichEdit.API.Layout

Namespace WindowsFormsApplication1.DocumentLayoutHelper
	Public Class CustomDocumentLayoutVisitor
		Inherits DevExpress.XtraRichEdit.API.Layout.LayoutVisitor
		Public ParagraphsCount As Integer = 0
		Public TextLinesCount As Integer = 0
		Public WordsCount As Integer = 0
		Public TablesCount As Integer = 0
		Public TextBoxesCount As Integer = 0
		Public ImagesCount As Integer = 0

		Protected Overrides Sub VisitTable(ByVal table As LayoutTable)
			TablesCount += 1
			MyBase.VisitTable(table)
		End Sub

		Protected Overrides Sub VisitTextBox(ByVal textBox As LayoutTextBox)
			TextBoxesCount += 1
			MyBase.VisitTextBox(textBox)
		End Sub

		Protected Overrides Sub VisitFloatingObjectAnchorBox(ByVal floatingObjectAnchorBox As FloatingObjectAnchorBox)
			Dim objectType As LayoutType = floatingObjectAnchorBox.FloatingObjectBox.Type
            If objectType.Equals(LayoutType.FloatingPicture) Then
                ImagesCount += 1
            End If
			MyBase.VisitFloatingObjectAnchorBox(floatingObjectAnchorBox)
		End Sub

		Protected Overrides Sub VisitInlinePictureBox(ByVal inlinePictureBox As InlinePictureBox)
			ImagesCount += 1
			MyBase.VisitInlinePictureBox(inlinePictureBox)
		End Sub

		Protected Overrides Sub VisitPlainTextBox(ByVal plainTextBox As PlainTextBox)
			WordsCount += 1
			MyBase.VisitPlainTextBox(plainTextBox)
		End Sub

		Protected Overrides Sub VisitParagraphMarkBox(ByVal paragraphMarkBox As PlainTextBox)
			ParagraphsCount += 1
			MyBase.VisitParagraphMarkBox(paragraphMarkBox)
		End Sub

		Protected Overrides Sub VisitRow(ByVal row As LayoutRow)
			TextLinesCount += 1
			MyBase.VisitRow(row)
		End Sub
	End Class
End Namespace
