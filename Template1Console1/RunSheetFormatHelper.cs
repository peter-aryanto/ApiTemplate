namespace Template1Console1.Utils;

public class RunSheetFormatHelper
{
  internal static string ExtractDocIdentifier(string pageText)
  {
    var docType = DetectDocType(pageText);
    return ExtractDocIdentifier(docType, pageText);
  }

  private static SdhTemplateExternalId DetectDocType(string pageText)
  {
    return pageText.IndexOf("Batch") == -1 ? SdhTemplateExternalId.RunSheet : SdhTemplateExternalId.OutputLabel;
  }

  private static string ExtractDocIdentifier(SdhTemplateExternalId docType, string pageText)
  {
    return docType == SdhTemplateExternalId.RunSheet
      ? ExtractDocIdentifier("Stream Name: ", pageText)
      : ExtractDocIdentifier("Batch: ", pageText);
  }

  private static string ExtractDocIdentifier(string docIdentifierKey, string pageText)
  {
    var identifierStartPos = pageText.IndexOf(docIdentifierKey);
    var identifierEndPos = pageText.IndexOf(Environment.NewLine, identifierStartPos + 1);
    var identifierLength = identifierEndPos - identifierStartPos;
    if (identifierLength - docIdentifierKey.Length <= 3)
    {
      identifierEndPos = pageText.IndexOf(Environment.NewLine, identifierEndPos + 1);
      identifierLength = identifierEndPos - identifierStartPos;
    }
    var identifier = pageText.Substring(identifierStartPos, identifierLength);
    return identifier;
  }
}