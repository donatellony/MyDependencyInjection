using MyDependencyInjectionLibrary;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddTransient<IDateTimeProvider, DateTimeProvider>();

services.AddSingleton<DateTimeProvider>();

var serviceProvider = services.BuildServiceProvider();

var dateTimeProvider = serviceProvider.GetService<IDateTimeProvider>();
Console.WriteLine(dateTimeProvider.Now.ToString());