using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var output = logic.Get(5);
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
