using System.Text.Json;
using System.Text.Json.Serialization;

namespace Template1Console1.Utils;

public class JsonSerializerUtils
{
    public static string? Serialize(object? o)
    {
        if (o == null)
        {
            return null;
        }

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };
        var output = System.Text.Json.JsonSerializer.Serialize(o, jsonSerializerOptions);
        return output;
    }
}