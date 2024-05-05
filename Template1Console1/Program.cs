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

// for (var i = 1; i <= pages.Count; ++i)
// {
  var text = sourcePdf.GetPageText(3);
  // var text = source.GetPageText(i);
  // var docType = RunSheetFormatHelper.DetectDocType(text);
  var docIdentifier = RunSheetFormatHelper.ExtractDocIdentifier(text);

//   break;
// }

Show(DateTime.UtcNow);
