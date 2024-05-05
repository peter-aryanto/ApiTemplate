// See https://aka.ms/new-console-template for more information
using Template1Console1.Utils;

// using DevExpress.Pdf;

var Show = (object? o) => Template1Console1.Utils.ConsoleUtils.Show(o);

// await Demo.RunTemplate1LibraryCallsAsync();

// await Demo.RunWebUtilsAsync();

var sourcePdf = new DevExpress.Pdf.PdfDocumentProcessor();
sourcePdf.LoadDocument(@"..\..\..\..\..\..\..\Files\TestRunSheetOutputLabel_V1.0_22 Mar_20240502_1015.pdf", true);
var sourceDoc = sourcePdf.Document;
var sourcePages = sourceDoc.Pages;

var targetPdf = new DevExpress.Pdf.PdfDocumentProcessor();
targetPdf.CreateEmptyDocument();
var targetDoc = targetPdf.Document;
var targetPages = targetDoc.Pages;

SdhTemplateExternalId prevDocType = SdhTemplateExternalId.RunSheet;
string? prevDocIdentifier = null;
var startPageIndex = -1;
var endPageIndex = -1;
var fillerPageIndex = -1; // e.g. the checklist page in run sheet, which will be inserted into every even page.
for (var i = 0; i < sourcePages.Count; ++i)
{
  var pageNumber = i + 1;
  // var text = sourcePdf.GetPageText(3);
  var text = sourcePdf.GetPageText(pageNumber);
  var docType = RunSheetFormatHelper.DetectDocType(text);
  var docIdentifier = RunSheetFormatHelper.ExtractDocIdentifier(docType, text);

  if (docIdentifier != prevDocIdentifier)
  {
    if (prevDocType == SdhTemplateExternalId.RunSheet && startPageIndex >= 0)
    {
      endPageIndex = Math.Max(i - 2, startPageIndex);
      // endPageIndex = 3;
      fillerPageIndex = i - 1;
      for (var j = startPageIndex; j <= endPageIndex; ++j)
      {
        if (j == fillerPageIndex)
        {
          continue;
        }

        targetPages.Add(sourcePages[j]);
        targetPages.Add(sourcePages[fillerPageIndex]);
      }
      // break;
    }

    if (docType != SdhTemplateExternalId.RunSheet)
    {
      targetPages.Add(sourcePages[i]);
    }

    if (docType == SdhTemplateExternalId.RunSheet)
    {
      startPageIndex = i;
    }
  }

  // targetPages.Add(sourcePages[i]);
  // break;

  prevDocType = docType;
  prevDocIdentifier = docIdentifier;
}

var timestamp = DateTime.UtcNow.ToString("yyyyMMddTHHmmss");
targetPdf.SaveDocument(@$"..\..\..\..\..\..\..\Files\target{timestamp}.pdf");

Show(DateTime.UtcNow);
