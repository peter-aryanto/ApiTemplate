// See https://aka.ms/new-console-template for more information
using Template1Console1.Utils;

var Show = (object? o) => Template1Console1.Utils.ConsoleUtils.Show(o);

await Demo.RunTemplate1LibraryCallsAsync();

await Demo.RunWebUtilsAsync();

Show(DateTime.UtcNow);
