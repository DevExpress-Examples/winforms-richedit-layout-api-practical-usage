# Document Layout API - Practical usage


<p>This example demonstrates how RichEditControl's DocumentLayout API can be used to collect information about a currently clicked document element (as well as its parent elements):<br /><a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument114069">Layout API</a><br /><br /></p>
<p>Depending on the type (text block, image, table cell, textbox, etc.) of a clicked element, the following information can be analyzed:<br /><strong>content, content bounds, position on a page, page bounds, current page index, a number of images/textboxes on a page, common text blocks count, etc.</strong></p>
<p><br />In this example, different approaches are used to get layout information about a current element:<br />- Use the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditAPILayoutDocumentLayout_GetElementtopic">DocumentLayout.GetElement</a> and <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraRichEditAPILayoutDocumentLayout_GetElement[T]topic">DocumentLayout.GetElement[T]</a> methods;<br />- Use the <strong>LayoutIterator</strong> class;<br />- Use a custom <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditAPILayoutLayoutVisitortopic">LayoutVisitor</a> descendant.<br /><br />To get layout information about the current element, <strong>set the caret</strong> inside a text block or table cell (or select an image) and click the "<strong>Analyze a current document layout</strong>" button.</p>

<br/>


