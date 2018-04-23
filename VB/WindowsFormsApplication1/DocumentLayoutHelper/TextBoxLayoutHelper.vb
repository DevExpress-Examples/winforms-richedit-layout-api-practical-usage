Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace WindowsFormsApplication1.DocumentLayoutHelper
	Public NotInheritable Class TextBoxLayoutHelper

		' get information about a current text box layout
		Private Sub New()
		End Sub
		Public Shared Function GetInformationAboutCurrentTextBox(ByVal subDocument As SubDocument, ByVal currentLayout As DocumentLayout, ByVal docPosition As DocumentPosition) As String
			Dim returnedInformation As String = "!A TEXTBOX CONTENT IS SELECTED!" & Constants.vbCrLf

            Dim layoutIterator As New LayoutIterator(currentLayout, subDocument.Range)
			Dim currentTextBoxPage As LayoutPage = Nothing
			Dim currentTextBox As LayoutTextBox = Nothing

			Do While layoutIterator.MoveNext()
				Dim element As LayoutElement = layoutIterator.Current
				If TypeOf element Is LayoutTextBox Then
					currentTextBox = (TryCast(element, LayoutTextBox))
					If TypeOf currentTextBox.Parent Is LayoutPage Then
						currentTextBoxPage = TryCast(currentTextBox.Parent, LayoutPage)
					End If
				End If
				If TypeOf element Is PlainTextBox Then
					Dim currentPlaintTextBox As PlainTextBox = TryCast(element, PlainTextBox)
					If currentPlaintTextBox.Range.Contains(docPosition.ToInt()) Then
						returnedInformation &= String.Format("Selected content: {0}" & Constants.vbCrLf, currentPlaintTextBox.Text)

						Dim currentRow As LayoutRow = TryCast(currentPlaintTextBox.Parent, LayoutRow)
						Dim currentLineIndex As Integer = currentTextBox.Rows.ToList().IndexOf(currentRow)
						returnedInformation &= String.Format("Line index: {0}" & Constants.vbCrLf, currentLineIndex + 1)
						returnedInformation &= String.Format("Selected block bounds: {0}" & Constants.vbCrLf, currentPlaintTextBox.Bounds)
						Exit Do
					End If
				End If
			Loop

			returnedInformation &= String.Format("TEXTBOX bounds: {0}" & Constants.vbCrLf, currentTextBox.Bounds)
			returnedInformation &= String.Format(Constants.vbCrLf & "!!Content information:" & Constants.vbCrLf)
			returnedInformation &= GetInformationAboutCurrentTextBoxContent(currentTextBox)

			If currentTextBoxPage IsNot Nothing Then
				returnedInformation &= PageLayoutHelper.GetInformationAboutCurrentPage(currentLayout, currentTextBoxPage, docPosition)
			End If
			Return returnedInformation
		End Function

		Public Shared Function GetInformationAboutCurrentTextBoxContent(ByVal currentTextBox As LayoutTextBox) As String
			Dim returnedInformation As String = ""
			Dim visitor As New CustomDocumentLayoutVisitor()
			visitor.Visit(currentTextBox)
			returnedInformation &= String.Format("Lines count: {0}" & Constants.vbCrLf, visitor.TextLinesCount)
            returnedInformation &= String.Format("Words count: {0}" & Constants.vbCrLf, visitor.WordsCount)

			returnedInformation &= String.Format("Plain text: {0}" & Constants.vbCrLf, currentTextBox.Document.GetText(currentTextBox.Document.Range))
			Return returnedInformation
		End Function
	End Class
End Namespace
