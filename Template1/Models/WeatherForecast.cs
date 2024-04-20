namespace Template1.Models;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public static readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
