using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template1.Entities;
using Template1.Logics;
using Template1.Models;

namespace Template1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastLogic logic;

        public WeatherForecastController(IWeatherForecastLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<WeatherForecastController>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var output = logic.Get(3);
            return output;
        }

        [HttpGet("g")]
        public async Task<string> GetGoogle()
        {
            var output = await logic.GetGoogleAsync();
            return output;
        }

        [HttpGet("create")]
        public async Task<KeyValue> CreateInDbAsync([FromQuery]string val1, [FromQuery]string val2)
        {
            //qwe1
            var output = await logic.CreateKeyValueAsync(val1, val2);
            return output;
        }

        /*
// LinqPad
var Ser = (object o) => System.Text.Json.JsonSerializer.Serialize(o);
var Show = (object o) =>
{
    if (o!= null && o is not string && !o.GetType().IsPrimitive)
//        o = System.Text.Json.JsonSerializer.Serialize(o);
        o = Ser(o);

	Console.WriteLine($"|{o?.ToString()}|{Environment.NewLine}");
};

var client = new RestClient(new Uri("http://localhost:5000"));
var req = new RestRequest("api/weatherforecast/create", Method.Post);
req.AddParameter("application/json", Ser(new {Value1 = "Val 11", Value2 = "Val 22"}), ParameterType.RequestBody);
var res = await client.ExecuteAsync(req);

//res.StatusCode.Dump();
//res.Content.Dump();
Show(res.StatusCode);
Show(res.Content);
        */
        // [HttpPost("create")]
        // // public async Task<KeyValue> CreateInDbAsync([FromBody]System.Text.Json.JsonElement inputJson)
        // public async Task<KeyValue> CreateInDbAsync([FromBody]KeyValue keyValue)
        // {
        //     //qwe1
        //     // var output = await logic.CreateKeyValueAsync(inputJson["val1"], inputJson["val2"]);
        //     var output = await logic.CreateKeyValueAsync(keyValue.Value1, keyValue.Value2);
        //     return output;
        // }

        [HttpGet("read")]
        public async Task<List<KeyValue>> ReadFromDbAsync()
        {
            var output = await logic.GetKeyValuesAsync();
            // output.Add(new() {KeyValueId = 1, Key = "Key1", Value1 = "Value1", Value2 = "Value2"});
            return output;
        }

        // GET api/<WeatherForecastController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WeatherForecastController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherForecastController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherForecastController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
