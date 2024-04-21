namespace Template1.Entities;

public class KeyValue
{
  public KeyValue()
  {
    AdditionalInfos = new HashSet<AdditionalInfo>();
    Key = Guid.NewGuid().ToString();
  }

  public int KeyValueId { get; set; }
  public string Key { get; set; }
  public string? Value1 { get; set; }
  public string? Value2 { get; set; }

  public ICollection<AdditionalInfo> AdditionalInfos { get; private set; }
}