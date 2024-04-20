// See https://aka.ms/new-console-template for more information
// using Template1.Logics;
using RestSharp;

void Show(object o)
{
    // if (o != null)
    // {
    //     var type = o.GetType();
    //     if (type.IsPrimitive)
    //     {
    //         o = System.Text.Json.JsonSerializer.Serialize(o);
    //     }
    // }
    if (o != null && o is not string && !o.GetType().IsPrimitive)
    {
        o = System.Text.Json.JsonSerializer.Serialize(o);
    }
    Console.WriteLine($"|{o?.ToString()}|{Environment.NewLine}");
}

// var forecast = new WeatherForecastLogic().Get();
// Show(forecast);

var restClient = new RestClient(new Uri("http://localhost:5000"));
var restRequest = new RestRequest("api/weatherforecast", Method.Get);
var res = await restClient.ExecuteAsync(restRequest);
// var resTask = restClient.ExecuteAsync(restRequest);
// resTask.Wait();
// var res = resTask.Result;
Show(res.StatusCode);
Show(res.Content);
restRequest = new RestRequest("weatherforecast0", Method.Get);
res = await restClient.ExecuteAsync(restRequest);
Show(res.StatusCode);
Show(res.Content);
// restClient = new RestClient(new Uri("https://www.google.com"));
// restRequest = new RestRequest(string.Empty, Method.Get);
// res = await restClient.ExecuteAsync(restRequest);
// Show(res.StatusCode);
// const string gs = "Google Search";
// Show(res.Content.Substring(res.Content.IndexOf(gs), gs.Length));

Show(DateTime.UtcNow);
