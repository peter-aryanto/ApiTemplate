using Template1.Models;

namespace Template1.Logics;

public interface IWeatherForecastLogic
{
    public IEnumerable<WeatherForecast> Get(int dayCount = 5);
}

public class WeatherForecastLogic : IWeatherForecastLogic
{
    public IEnumerable<WeatherForecast> Get(int dayCount = 5)
    {
        var forecast =  Enumerable.Range(1, dayCount).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                WeatherForecast.summaries[Random.Shared.Next(WeatherForecast.summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}