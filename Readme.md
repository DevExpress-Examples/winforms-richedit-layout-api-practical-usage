<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128609100/15.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T266080)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomDocumentLayoutVisitor.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/CustomDocumentLayoutVisitor.cs) (VB: [CustomDocumentLayoutVisitor.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/CustomDocumentLayoutVisitor.vb))
* [DocumentElementLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/DocumentElementLayoutHelper.cs) (VB: [DocumentElementLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/DocumentElementLayoutHelper.vb))
* [PageLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/PageLayoutHelper.cs) (VB: [PageLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/PageLayoutHelper.vb))
* [RichEditDocumentLayoutAnalyzer.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/RichEditDocumentLayoutAnalyzer.cs) (VB: [RichEditDocumentLayoutAnalyzer.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/RichEditDocumentLayoutAnalyzer.vb))
* [TextBoxLayoutHelper.cs](./CS/WindowsFormsApplication1/DocumentLayoutHelper/TextBoxLayoutHelper.cs) (VB: [TextBoxLayoutHelper.vb](./VB/WindowsFormsApplication1/DocumentLayoutHelper/TextBoxLayoutHelper.vb))
* [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb))
<!-- default file list end -->
# Document Layout API - Practical usage


<p>This example demonstrates how RichEditControl's DocumentLayout API can be used to collect information about a currently clicked document element (as well as its parent elements):<br /><a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument114069">Layout API</a><br /><br /></p>
<p>Depending on the type (text block, image, table cell, textbox, etc.) of a clicked element, the following information can be analyzed:<br /><strong>content, content bounds, position on a page, page bounds, current page index, a number of images/textboxes on a page, common text blocks count, etc.</strong></p>
<p><br />In this example, different approaches are used to get layout information about a current element:<br />- Use the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditAPILayoutDocumentLayout_GetElementtopic">DocumentLayout.GetElement</a> and <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditAPILayoutDocumentLayout_GetElement[T]topic">DocumentLayout.GetElement[T]</a> methods;<br />- Use the <strong>LayoutIterator</strong> class;<br />- Use a custom <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditAPILayoutLayoutVisitortopic">LayoutVisitor</a> descendant.<br /><br />To get layout information about the current element, <strong>set the caret</strong> inside a text block or table cell (or select an image) and click the "<strong>Analyze a current document layout</strong>" button.</p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-layout-api-practical-usage&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-layout-api-practical-usage&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
