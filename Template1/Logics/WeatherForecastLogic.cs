using RestSharp;
using Template1.Models;
using Template1.Entities;
using Template1.Entities.Queries;

namespace Template1.Logics;

public interface IWeatherForecastLogic
{
    // public const int DefaultDayCount = 3;

    // public IEnumerable<WeatherForecast> Get(int dayCount = DefaultDayCount);
    public IEnumerable<WeatherForecast> Get(int? dayCount = null);
    public Task<string> GetGoogleAsync(IRestClient? restClient = null);
    public Task<List<KeyValue>> GetKeyValuesAsync();
}

public class WeatherForecastLogic : IWeatherForecastLogic
{
    public const int DefaultDayCount = 3;

    private readonly IKeyValueQueries keyValueQueries;

    public WeatherForecastLogic(IKeyValueQueries keyValueQueries)
    {
        this.keyValueQueries = keyValueQueries;
    }

    // public WeatherForecastLogic(IRestClient restClient)
    // {
    //     this.RestClient = restClient;
    // }

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

    private static IRestClient CreateRestClient()
    {
        const string GoogleUrl = "https://www.google.com";
        return new RestClient(new Uri(GoogleUrl));
    }

    private static string Show(object o)
    {
        if (o != null && o is not string && !o.GetType().IsPrimitive)
        {
            o = System.Text.Json.JsonSerializer.Serialize(o);
        }
        return $"{o?.ToString()}";
    }

    public async Task<string> GetGoogleAsync(IRestClient? restClient = null)
    {
        // restClient = new RestClient(new Uri("https://www.google.com"));
        restClient ??= CreateRestClient();
        var restRequest = new RestRequest(string.Empty, Method.Get);
        var res = await restClient.ExecuteAsync(restRequest);
        const string gs = "Google Search";
        return Show($"{res.StatusCode} - {res.Content?.Substring(res.Content.IndexOf(gs), gs.Length)}");
    }

    public async Task<List<KeyValue>> GetKeyValuesAsync()
    {
        var output = await keyValueQueries.GetAsync();
        return output;
    }
}