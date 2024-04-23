// See https://aka.ms/new-console-template for more information
using Template1.Logics;
using Template1Console1.Utils;

var Show = (object? o) => ConsoleUtils.Show(o);

// Demo Library Template1
var forecast = new WeatherForecastLogic(null).Get();
Show(forecast);

// await Demo.RunWebUtilsAsync();

Show(DateTime.UtcNow);
