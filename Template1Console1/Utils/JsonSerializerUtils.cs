using System.Text.Json;
using System.Text.Json.Serialization;

namespace Template1Console1.Utils;

public class JsonSerializerUtils
{
    public static bool PreserveDefaultOptions { get; set; } = true;
    public static bool Indented { get; set; } = false;

    private static readonly bool DefaultIndented = false;

    public static string? Serialize(object? o)
    {
        if (o == null)
        {
            return null;
        }

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = Indented,
        };
        var output = System.Text.Json.JsonSerializer.Serialize(o, jsonSerializerOptions);

        ResetDefaultOptions();
        return output;
    }

    private static void ResetDefaultOptions()
    {
        if (!PreserveDefaultOptions)
        {
            return;
        }

        Indented = DefaultIndented;
    }
}