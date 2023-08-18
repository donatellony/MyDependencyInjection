using MyDependencyInjectionConsoleApp.Services;
using MyDependencyInjectionLibrary;

// WARNING - USING MULTIPLE SERVICE PROVIDERS CAN BE DANGEROUS AND LEAD TO UNPREDICTABLE ERRORS!!!!!!
// I'm using them ONLY to demonstrate different lifetimes
{
    Console.WriteLine("Everything as the transient");
    var services = new ServiceCollection();

    services.AddTransient<IGuidGenerator, GuidGenerator>();
    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    services.AddTransient<IConsoleWriter, ConsoleWriter>();


    var serviceProvider = services.BuildServiceProvider();

    var guidGenerator = serviceProvider.GetService<IGuidGenerator>();
    var guidGenerator2 = serviceProvider.GetService<IGuidGenerator>();
    Console.WriteLine(guidGenerator!.GetGuidAndNowTicks());
    Console.WriteLine(guidGenerator2!.GetGuidAndNowTicks());

    var consoleWriter = serviceProvider.GetService<IConsoleWriter>();
    consoleWriter!.WriteSomeSpecificInfo();
    Console.WriteLine();
}

{
    Console.WriteLine("Only DateTimeProvider is a singleton, \nticks have to be the same, but GUIDs - nope");
    var services = new ServiceCollection();

    services.AddTransient<IGuidGenerator, GuidGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddTransient<IConsoleWriter, ConsoleWriter>();

    var serviceProvider = services.BuildServiceProvider();

    var guidGenerator = serviceProvider.GetService<IGuidGenerator>();
    var guidGenerator2 = serviceProvider.GetService<IGuidGenerator>();
    Console.WriteLine(guidGenerator!.GetGuidAndNowTicks());
    Console.WriteLine(guidGenerator2!.GetGuidAndNowTicks());

    var consoleWriter = serviceProvider.GetService<IConsoleWriter>();
    consoleWriter!.WriteSomeSpecificInfo();
    Console.WriteLine();
}

{
    Console.WriteLine("Only GuidGenerator is a singleton, \nDateTimeProvider will be \"converted\" from transient to singleton because of that");
    var services = new ServiceCollection();

    services.AddSingleton<IGuidGenerator, GuidGenerator>();
    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    services.AddTransient<IConsoleWriter, ConsoleWriter>();

    var serviceProvider = services.BuildServiceProvider();

    var guidGenerator = serviceProvider.GetService<IGuidGenerator>();
    var guidGenerator2 = serviceProvider.GetService<IGuidGenerator>();
    Console.WriteLine(guidGenerator!.GetGuidAndNowTicks());
    Console.WriteLine(guidGenerator2!.GetGuidAndNowTicks());

    var consoleWriter = serviceProvider.GetService<IConsoleWriter>();
    consoleWriter!.WriteSomeSpecificInfo();
    Console.WriteLine();
}