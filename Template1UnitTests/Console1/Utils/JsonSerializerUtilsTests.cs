using Template1Console1.Utils;

namespace Template1UnitTests.Console1;

public class JsonSerializerUtilsTests
{
    private record TestRecord(string StringValue);

    [Fact]
    // public void Serialize_WhenInputIsNull_ShouldReturnNull()
    public void Serialize_ShouldReturnJsonString()
    {
        var output = JsonSerializerUtils.Serialize(null);
        Assert.Null(output);

        var testRecord = new TestRecord("Hello World");
        output = JsonSerializerUtils.Serialize(testRecord);
        Assert.Equal($"{{\"StringValue\":\"{testRecord.StringValue}\"}}", output);
    }
}