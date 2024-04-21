namespace Template1.Entities;

public class AdditionalInfo
{
  public int AdditionalInfoId { get; set; }
  public int KeyValueId { get; set; }

  public required KeyValue KeyValue { get; set; }
}