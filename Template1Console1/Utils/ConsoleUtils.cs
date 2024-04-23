namespace Template1Console1.Utils;

public class ConsoleUtils
{
    public static void Show(object? o, bool newLineEnding = true)
    {
        if (o != null && o is not string && !o.GetType().IsPrimitive)
        {
            o = System.Text.Json.JsonSerializer.Serialize(o);
        }
        Console.WriteLine($"|{o?.ToString()}|{(newLineEnding ? Environment.NewLine : string.Empty)}");
    }
}