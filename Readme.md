<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128609100/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T266080)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Rich Text Editor for WinForms: Document Layout API - Practical Usage

This example demonstrates how to use RichEditControl's layout API  to collect information about a currently clicked document element (and its parent elements).

## Implementation Details

This example shows the following approaches to get layout information about a current element:

* Use the [DocumentLayout.GetElement](https://docs.devexpress.com/OfficeFileAPI/DevExpress.XtraRichEdit.API.Layout.DocumentLayout.GetElement.overloads) method overloads;
* Use the `LayoutIterator` class;
* Use a custom [LayoutVisitor](https://docs.devexpress.com/OfficeFileAPI/DevExpress.XtraRichEdit.API.Layout.LayoutVisitor) descendant.

To get layout information about the current element, set the caret inside a text block or table cell (or select an image) and click the **Analyze current document layout** button.

Depending on the type (text block, image, table cell, textbox, etc.) of a clicked element, you can retrieve the following information:

* Content
* Content bounds
* Position on a page
* Page bounds
* Current page index
* Number of images/text boxes on a page
* Common text blocks count, etc.

## Files to Review

| C# | Visual Basic |
|---|---|
| [CustomDocumentLayoutVisitor.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/CustomDocumentLayoutVisitor.cs) |  [CustomDocumentLayoutVisitor.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/CustomDocumentLayoutVisitor.vb) |
| [DocumentElementLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/DocumentElementLayoutHelper.cs) | [DocumentElementLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/DocumentElementLayoutHelper.vb) |
| [PageLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/PageLayoutHelper.cs) | [PageLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/PageLayoutHelper.vb) |
| [RichEditDocumentLayoutAnalyzer.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/RichEditDocumentLayoutAnalyzer.cs) | [RichEditDocumentLayoutAnalyzer.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/RichEditDocumentLayoutAnalyzer.vb) |
| [TextBoxLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/TextBoxLayoutHelper.cs) | [TextBoxLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/TextBoxLayoutHelper.vb) |
| [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) | [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb) |

## Documentation

* [Layout API](https://docs.devexpress.com/WindowsForms/114069/controls-and-libraries/rich-text-editor/page-layout/layout-api)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-layout-api-practical-usage&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-layout-api-practical-usage&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
