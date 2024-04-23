namespace Template1Console1.Utils;

public class ConsoleUtils
{
    public static void Show(object? o, bool newLineEnding = true)
    {
        if (o != null && o is not string && !o.GetType().IsPrimitive)
        {
            // using System.Text.Json;
            // var jsonSerializerOptions = new JsonSerializerOptions
            // {
            //     using System.Text.Json.Serialization;
            //     ReferenceHandler = ReferenceHandler.IgnoreCycles,
            // };
            // o = System.Text.Json.JsonSerializer.Serialize(o, jsonSerializerOptions);
            o = JsonSerializerUtils.Serialize(o);
        }
        Console.WriteLine($"|{o?.ToString()}|{(newLineEnding ? Environment.NewLine : string.Empty)}");
    }
}