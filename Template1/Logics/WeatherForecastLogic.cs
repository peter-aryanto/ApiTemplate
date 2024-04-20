using Template1.Models;

namespace Template1.Logics;

public interface IWeatherForecastLogic
{
    // public const int DefaultDayCount = 3;

    // public IEnumerable<WeatherForecast> Get(int dayCount = DefaultDayCount);
    public IEnumerable<WeatherForecast> Get(int? dayCount = null);
}

public class WeatherForecastLogic : IWeatherForecastLogic
{
    public const int DefaultDayCount = 3;

    public IEnumerable<WeatherForecast> Get(int? dayCount = null)
    {
        int validDayCount = dayCount == null || dayCount <= 0
          ? DefaultDayCount
          : dayCount.Value;

        var forecast =  Enumerable.Range(1, validDayCount).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                WeatherForecast.Summaries[Random.Shared.Next(WeatherForecast.Summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}