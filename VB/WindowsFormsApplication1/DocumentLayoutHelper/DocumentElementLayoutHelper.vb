Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Office.Utils
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace WindowsFormsApplication1.DocumentLayoutHelper
	Public NotInheritable Class DocumentElementLayoutHelper

		' get information about a current regular text block
		Private Sub New()
		End Sub
		Public Shared Function GetInformationAboutRegularTextBlock(ByVal currentTextBlock As PlainTextBox, ByVal currentDocument As Document) As String
			Dim returnedInformation As String = "!A REGULAR TEXT BLOCK IS SELECTED!" & Constants.vbCrLf
			returnedInformation &= String.Format("Content: {0}" & Constants.vbCrLf, currentTextBlock.Text)

			Dim currentRow As LayoutRow = TryCast(currentTextBlock.Parent, LayoutRow)
			Dim currentColumn As LayoutColumn = TryCast(currentRow.Parent, LayoutColumn)
			Dim currentLineIndex As Integer = currentColumn.Rows.ToList().IndexOf(currentRow)
			returnedInformation &= String.Format("Line index: {0}" & Constants.vbCrLf, currentLineIndex + 1)
			returnedInformation &= String.Format("Start position: {0}" & Constants.vbCrLf, currentTextBlock.Range.Start)
			returnedInformation &= String.Format("End position: {0}" & Constants.vbCrLf, currentTextBlock.Range.Start + currentTextBlock.Range.Length)

			Dim currentParagraphIndex As Integer = currentDocument.Paragraphs.ToList().IndexOf(currentDocument.Paragraphs.Get(currentDocument.CreatePosition(currentTextBlock.Range.Start)))
			returnedInformation &= String.Format("Paragrpah index: {0}" & Constants.vbCrLf, currentParagraphIndex + 1)
			returnedInformation &= String.Format("Text block bounds: {0}" & Constants.vbCrLf, currentTextBlock.Bounds)

			Return returnedInformation
		End Function

        ' get information about a current floating object
        Public Shared Function GetInformationAboutCurrentFloatingObject(ByVal currentObjectAnchor As FloatingObjectAnchorBox, currentLayout As DocumentLayout) As String
            Dim currentFloatingObject As LayoutFloatingObject = currentObjectAnchor.FloatingObjectBox
            Dim returnedInformation As String = ""
            If currentFloatingObject.Type = LayoutType.FloatingPicture Then
                returnedInformation = "!A FLOATING PICTURE IS SELECTED!" & Constants.vbCrLf
            ElseIf currentFloatingObject.Type.Equals(LayoutType.TextBox) Then
                returnedInformation = "!A TEXT BOX IS SELECTED!" & Constants.vbCrLf
            End If

            returnedInformation &= String.Format("Anchor location: {0}" & Constants.vbCrLf, currentObjectAnchor.Bounds.Location)
            returnedInformation &= String.Format("Object bounds: {0}" & Constants.vbCrLf, currentFloatingObject.Bounds)
            returnedInformation &= String.Format("Rotation: {0}" & Constants.vbCrLf, currentFloatingObject.RotationAngle)

            If currentFloatingObject.Type.Equals(LayoutType.TextBox) Then
                Dim currentTextBox As LayoutTextBox = TryCast(currentFloatingObject, LayoutTextBox)
                returnedInformation &= String.Format(Constants.vbCrLf & "!!Content information:" & Constants.vbCrLf)
                returnedInformation &= TextBoxLayoutHelper.GetInformationAboutCurrentTextBoxContent(currentTextBox, currentLayout)
            ElseIf currentFloatingObject.Type = LayoutType.FloatingPicture Then
                Dim currentFloatingPicture As LayoutFloatingPicture = TryCast(currentFloatingObject, LayoutFloatingPicture)
                returnedInformation &= String.Format(Constants.vbCrLf & "!!Image properties:" & Constants.vbCrLf)
                returnedInformation &= GetInformationAboutOfficeImage(currentFloatingPicture.Image)
            End If

            Return returnedInformation
        End Function

        ' get information about a current inline picture
        Public Shared Function GetInformationAboutCurrentInlinePicture(ByVal inlinePicture As InlinePictureBox) As String
			Dim returnedInformation As String = "!AN INLINE IMAGE IS SELECTED!" & Constants.vbCrLf
			returnedInformation &= String.Format("Start position: {0}" & Constants.vbCrLf, inlinePicture.Range.Start)
			returnedInformation &= GetInformationAboutOfficeImage(inlinePicture.Image)
			Return returnedInformation
		End Function

		' get information about a current table cell
		Public Shared Function GetInformationAboutCurrentTableCell(ByVal currentDocument As Document, ByVal tableCell As LayoutTableCell) As String
			Dim returnedInformation As String = "!A TABLE CELL IS SELECTED!" & Constants.vbCrLf
			Dim cellText As String = currentDocument.GetText(currentDocument.CreateRange(tableCell.Range.Start, tableCell.Range.Length - 1))
			returnedInformation &= String.Format("Cell's content: {0}" & Constants.vbCrLf, cellText)

			Dim currentRow As LayoutTableRow = TryCast(tableCell.Parent, LayoutTableRow)
			If currentRow Is Nothing Then
				Return returnedInformation
			End If
			Dim currentTable As LayoutTable = TryCast(currentRow.Parent, LayoutTable)
			If currentTable Is Nothing Then
				Return returnedInformation
			End If

			returnedInformation &= String.Format("Row index: {0}" & Constants.vbCrLf, currentTable.TableRows.ToList().IndexOf(currentRow) + 1)
			For i As Integer = 0 To currentRow.TableCells.Count - 1
				If currentRow.TableCells(i) Is tableCell Then
					returnedInformation &= String.Format("Column index: {0}" & Constants.vbCrLf, i + 1)
					Exit For
				End If
			Next i

			returnedInformation &= String.Format(Constants.vbCrLf & "!Table information:" & Constants.vbCrLf)
			returnedInformation &= String.Format("Row count: {0}" & Constants.vbCrLf, currentTable.TableRows.Count)
			Dim maxCellsCount As Integer = 0
			For i As Integer = 0 To currentTable.TableRows.Count - 1
				If currentTable.TableRows(i).TableCells.Count > maxCellsCount Then
					maxCellsCount = currentTable.TableRows(i).TableCells.Count
				End If
			Next i
			returnedInformation &= String.Format("Column count: {0}" & Constants.vbCrLf, maxCellsCount)
			Return returnedInformation
		End Function

		' get information about a current inline picture
		Public Shared Function GetInformationAboutOfficeImage(ByVal officeImage As OfficeImage) As String
			Dim returnedInformation As String = ""
			returnedInformation &= String.Format("Format: {0}" & Constants.vbCrLf, officeImage.RawFormat)
			returnedInformation &= String.Format("Original size: {0}" & Constants.vbCrLf, officeImage.SizeInPixels)
			Return returnedInformation
		End Function
	End Class
End Namespace
