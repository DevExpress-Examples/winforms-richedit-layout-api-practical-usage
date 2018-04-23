Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace WindowsFormsApplication1.DocumentLayoutHelper
	Public NotInheritable Class PageLayoutHelper
		Private Sub New()
		End Sub
		Public Shared Function GetInformationAboutCurrentPage(ByVal currentDocumentLayout As DocumentLayout, ByVal currentPage As LayoutPage, ByVal currentPosition As DocumentPosition) As String
			Dim returnedInformation As String = ""

			Dim currentPageIndex As Integer = currentDocumentLayout.GetPageIndex(currentPage)
			Dim totalPageCount As Integer = currentDocumentLayout.GetFormattedPageCount()

			' get information about page content using a LayoutVisitor descendant
			Dim visitor As New CustomDocumentLayoutVisitor()
			visitor.Visit(currentPage)

			' get information about page bounds using PageArea properties
			Dim pageAreaProperties As PageAreaProperties = CaculatePageAreaProperties(currentPage.PageAreas(0), currentPosition)

			returnedInformation &= String.Format(Constants.vbCrLf & "Page: {0} of {1}" & Constants.vbCrLf, currentPageIndex + 1, totalPageCount)
			returnedInformation &= String.Format("Current page range: {0} - {1}" & Constants.vbCrLf, currentPage.MainContentRange.Start, currentPage.MainContentRange.Start + currentPage.MainContentRange.Length - 1)
			returnedInformation &= String.Format("Images count: {0}" & Constants.vbCrLf, visitor.ImagesCount)
			returnedInformation &= String.Format("Tables count: {0}" & Constants.vbCrLf, visitor.TablesCount)
			returnedInformation &= String.Format("Text Boxes count: {0}" & Constants.vbCrLf, visitor.TextBoxesCount)
			returnedInformation &= String.Format("Paragraphs count: {0}" & Constants.vbCrLf, visitor.ParagraphsCount)
			returnedInformation &= String.Format("Text lines count: {0}" & Constants.vbCrLf, visitor.TextLinesCount)
			returnedInformation &= String.Format("Words (text blocks) count: {0}" & Constants.vbCrLf, visitor.WordsCount)

			returnedInformation &= String.Format(Constants.vbCrLf & "Column: {0} of {1}" & Constants.vbCrLf, pageAreaProperties.currentColumnIndex, pageAreaProperties.columnCount)
			returnedInformation &= String.Format("Current COLUMN content bounds: {0}" & Constants.vbCrLf, pageAreaProperties.currentColumnBounds)
			returnedInformation &= String.Format("Current PAGE content bounds: {0}" & Constants.vbCrLf, pageAreaProperties.currentPageBounds)

			Return returnedInformation
		End Function

		Public Shared Function CaculatePageAreaProperties(ByVal pageArea As LayoutPageArea, ByVal pos As DocumentPosition) As PageAreaProperties
			Dim pageAreaProperties As New PageAreaProperties()
			pageAreaProperties.columnCount = pageArea.Columns.Count

			pageAreaProperties.currentPageBounds.Location = pageArea.Columns(0).Bounds.Location

			For i As Integer = 0 To pageArea.Columns.Count - 1
				Dim currentColumnContentHeight As Integer = pageArea.Columns(i).Rows.Last.Bounds.Bottom - pageArea.Columns(i).Rows.First.Bounds.Top
				If pageArea.Columns(i).Range.Contains(pos.ToInt()) Then
					pageAreaProperties.currentColumnIndex = i
					pageAreaProperties.currentColumnBounds.Location = pageArea.Columns(i).Bounds.Location
					pageAreaProperties.currentColumnBounds.Width = pageArea.Columns(i).Bounds.Width
					pageAreaProperties.currentColumnBounds.Height = currentColumnContentHeight
				End If
				pageAreaProperties.currentPageBounds.Width += pageArea.Columns(i).Bounds.Width
				pageAreaProperties.currentPageBounds.Height = Math.Max(pageAreaProperties.currentPageBounds.Height, currentColumnContentHeight)
			Next i
			Return pageAreaProperties
		End Function
	End Class

	Public Class PageAreaProperties
		Public currentColumnIndex As Integer = 0
		Public columnCount As Integer = 0
		Public currentColumnBounds As Rectangle = Rectangle.Empty
		Public currentPageBounds As Rectangle = Rectangle.Empty
	End Class
End Namespace
