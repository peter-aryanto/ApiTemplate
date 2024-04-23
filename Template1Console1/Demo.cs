using Template1Console1.Template1;
using Template1.Entities.Queries;
using Template1.Logics;
using RestSharp;

namespace Template1Console1.Utils;

internal class Demo
{
    private static readonly Action<object?> Show = (object? o) => ConsoleUtils.Show(o);

    internal static async Task RunTemplate1LibraryCallsAsync()
    {
        // using var context1 = EntitiesHelper.CreateContext1();
        using var context1 = EntitiesHelper.CreateContext1FromPool();
        var keyValueQueries = new KeyValueQueries(context1);
        var weatherForecastsLogic = new WeatherForecastLogic(keyValueQueries);
        var forecast = weatherForecastsLogic.Get();
        Show(forecast);
        var keyValues = await weatherForecastsLogic.GetKeyValuesAsync();
        Show(keyValues);
    }

    internal static async Task RunWebUtilsAsync()
    {
        RestResponse res;

        res = await WebUtils.SendGetRequestAsync("http://localhost:5000/api/weatherforecast");
        if (!string.IsNullOrWhiteSpace(res.Content))
        {
            Show(res.StatusCode);
            Show(res.Content);
            return;
        }

        res = await WebUtils.SendGetRequestAsync("https://www.google.com");
        Show(res.StatusCode);
        const string ExpectedContentFromGoogle = "Google Search";
        var contentFromGoogle = res.Content;
        Show(res.Content?.IndexOf(ExpectedContentFromGoogle) > -1 ? $"...{ExpectedContentFromGoogle}..." : "NOT FOUND!!!");
    }
}