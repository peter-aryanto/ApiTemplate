using RestSharp;

namespace Template1Console1.Utils;

internal class Demo
{
    private static readonly Action<object?> Show = (object? o) => ConsoleUtils.Show(o);

    internal static async Task RunWebUtilsAsync()
    {
        RestResponse res;
        res = await WebUtils.SendGetRequestAsync("http://localhost:5000/api/weatherforecast");
        Show(res.StatusCode);
        Show(res.Content);
        // res = await WebUtils.SendGetRequestAsync("https://www.google.com");
        // Show(res.StatusCode);
        // Show(res.Content);
    }
}