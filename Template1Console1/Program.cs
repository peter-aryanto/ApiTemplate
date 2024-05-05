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

string prevDocIdentifier = null;
int startPage;
int endPage;
int fillerPage; // e.g. the checklist page in run sheet, which will be inserted into every even page.
for (var i = 0; i < sourcePages.Count; ++i)
{
  var pageNumber = i + 1;
  // var text = sourcePdf.GetPageText(3);
  var text = sourcePdf.GetPageText(pageNumber);
  var docType = RunSheetFormatHelper.DetectDocType(text);
  if (docType != SdhTemplateExternalId.RunSheet)
  {
    targetPages.Append(sourcePages[i]);
    continue;
  }

  var docIdentifier = RunSheetFormatHelper.ExtractDocIdentifier(docType, text);
  // if (docIdentifier != prevDocIdentifier)
  // {
  //   startPage
  // }

  targetPages.Add(sourcePages[i]);
  break;

  prevDocIdentifier = docIdentifier;
}

var timestamp = DateTime.UtcNow.ToString("yyyyMMddTHHmmss");
targetPdf.SaveDocument(@$"..\..\..\..\..\..\..\Files\target{timestamp}.pdf");

Show(DateTime.UtcNow);
