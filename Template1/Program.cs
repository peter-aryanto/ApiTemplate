using Template1;
using Template1.Logics;
using Template1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddContext1(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddScoped<IWeatherForecastLogic, WeatherForecastLogic>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast0", () =>
{
    // var forecast =  Enumerable.Range(1, 5).Select(index =>
    //     new WeatherForecast
    //     (
    //         DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //         Random.Shared.Next(-20, 55),
    //         summaries[Random.Shared.Next(summaries.Length)]
    //     ))
    //     .ToArray();
    // return forecast;
    var output = new WeatherForecastLogic().Get(5);
    return output;
})
.WithName("GetWeatherForecast0")
.WithOpenApi();

app.MapControllers();

app.Run();
