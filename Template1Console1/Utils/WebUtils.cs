using RestSharp;

namespace Template1Console1.Utils;

public class WebUtils
{
    public static async Task<RestResponse> SendGetRequestAsync(string url)
    {
/*
        var restClient = new RestClient(new Uri("http://localhost:5000"));
        // var restRequest = new RestRequest("api/weatherforecast", Method.Get);
        var restRequest = new RestRequest(string.Empty, Method.Get);
        var res = await restClient.ExecuteAsync(restRequest);
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
*/
        var restClient = new RestClient(new Uri(url));
        var restRequest = new RestRequest(string.Empty, Method.Get);
        var res = await restClient.ExecuteAsync(restRequest);
        return res;
    }
}