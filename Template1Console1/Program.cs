// See https://aka.ms/new-console-template for more information
using Template1.Logics;

void Show(object o = null)
{
    if (o != null && !o.GetType().IsPrimitive)
    {
        o = System.Text.Json.JsonSerializer.Serialize(o);
    }
    Console.WriteLine($"|{o?.ToString()}|{Environment.NewLine}");
}

var forecast = new WeatherForecastLogic().Get();
Show(forecast);
