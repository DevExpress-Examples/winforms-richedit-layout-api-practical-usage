Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace WindowsFormsApplication1.DocumentLayoutHelper
	Public NotInheritable Class RichEditDocumentLayoutAnalyzer
		Private Sub New()
		End Sub
		Public Shared Function GetInformationAboutRichEditDocumentLayout(ByVal currentDocument As Document, ByVal currentDocumentLayout As DocumentLayout) As String
			Dim subDocument As SubDocument = currentDocument.CaretPosition.BeginUpdateDocument()
			Dim docPosition As DocumentPosition = subDocument.CreatePosition(If(currentDocument.CaretPosition.ToInt() = 0, 0, currentDocument.CaretPosition.ToInt() - 1))

			Dim shapes As ReadOnlyShapeCollection = subDocument.Shapes.Get(subDocument.CreateRange(docPosition, 1))
			Dim images As ReadOnlyDocumentImageCollection = subDocument.Images.Get(subDocument.CreateRange(docPosition, 1))

			If shapes.Count = 0 AndAlso images.Count = 0 Then
				docPosition = subDocument.CreatePosition(currentDocument.CaretPosition.ToInt())
			End If

			Dim returnedInformation As String = ""
			' get infromation about a current document element
			returnedInformation &= GetInformationAboutCurrentDocumentElement(currentDocument, currentDocumentLayout, subDocument, docPosition)

			' collect information about CURRENT PAGE
			Dim layoutPosition As RangedLayoutElement = currentDocumentLayout.GetElement(Of RangedLayoutElement)(docPosition)
			If layoutPosition IsNot Nothing Then
				Dim currentPageIndex As Integer = currentDocumentLayout.GetPageIndex(layoutPosition)
				returnedInformation &= PageLayoutHelper.GetInformationAboutCurrentPage(currentDocumentLayout, currentDocumentLayout.GetPage(currentPageIndex), docPosition)
			End If

			currentDocument.CaretPosition.EndUpdateDocument(subDocument)

			Return returnedInformation
		End Function

		Public Shared Function GetInformationAboutCurrentDocumentElement(ByVal currentDocument As Document, ByVal currentDocumentLayout As DocumentLayout, ByVal currentSubDocument As SubDocument, ByVal docPosition As DocumentPosition) As String
            If currentSubDocument.GetSubDocumentType().Equals(SubDocumentType.TextBox) Then
                Return TextBoxLayoutHelper.GetInformationAboutCurrentTextBox(currentSubDocument, currentDocumentLayout, docPosition)
            End If
			If currentSubDocument.GetSubDocumentType() = SubDocumentType.Main Then

				Dim tableCell As RangedLayoutElement = currentDocumentLayout.GetElement(docPosition, LayoutType.TableCell)
				If tableCell IsNot Nothing Then
					' collect information about TABLE CELL
					Return DocumentElementLayoutHelper.GetInformationAboutCurrentTableCell(currentDocument, TryCast(tableCell, LayoutTableCell))
				End If

				Dim imageinline As RangedLayoutElement = currentDocumentLayout.GetElement(docPosition, LayoutType.InlinePictureBox)
				If imageinline IsNot Nothing Then
					' collect information about INLINE PICTURE
					Return DocumentElementLayoutHelper.GetInformationAboutCurrentInlinePicture(TryCast(imageinline, InlinePictureBox))
				End If


				Dim floatingObjectAnchor As RangedLayoutElement = currentDocumentLayout.GetElement(docPosition, LayoutType.FloatingObjectAnchorBox)
				If floatingObjectAnchor IsNot Nothing Then
                    ' collect information about FLOATING OBJECT
                    Return DocumentElementLayoutHelper.GetInformationAboutCurrentFloatingObject(TryCast(floatingObjectAnchor, FloatingObjectAnchorBox), currentDocumentLayout)
                End If

				Dim regularTextBlock As RangedLayoutElement = currentDocumentLayout.GetElement(docPosition, LayoutType.PlainTextBox)
				If regularTextBlock IsNot Nothing Then
					' collect information about REGULAR TEXT BLOCK
					Return DocumentElementLayoutHelper.GetInformationAboutRegularTextBlock(TryCast(regularTextBlock, PlainTextBox), currentDocument)
				End If
			End If
			Return ""
		End Function
	End Class
End Namespace
