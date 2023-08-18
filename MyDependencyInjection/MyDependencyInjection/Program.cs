using MyDependencyInjectionConsoleApp.Services;
using MyDependencyInjectionLibrary;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddTransient<IDateTimeProvider, DateTimeProvider>();
services.AddSingleton<IGuidGenerator, GuidGenerator>();
services.AddTransient<IDateTimeProvider, DateTimeProvider>();


var serviceProvider = services.BuildServiceProvider();

var guidGenerator = serviceProvider.GetService<IGuidGenerator>();
var guidGenerator2 = serviceProvider.GetService<IGuidGenerator>();
Console.WriteLine(guidGenerator.GetGuidAndNowTicks());
Console.WriteLine(guidGenerator2.GetGuidAndNowTicks());