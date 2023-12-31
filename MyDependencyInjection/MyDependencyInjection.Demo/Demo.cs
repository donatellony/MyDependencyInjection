using MyDependencyInjection.Library;
using MyDependencyInjection.Shared;
using MyDependencyInjection.Shared.Services;

namespace MyDependencyInjection.Demo;

public sealed class Demo : IDemo
{
    public void Run()
    {
        throw new NotImplementedException();
    }

    public void FullTransientDemo()
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

    public void DateTimeProviderSingletonDemo()
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

    public void GuidGeneratorSingletonDemo()
    {
        Console.WriteLine(
            "Only GuidGenerator is a singleton, \nDateTimeProvider will be \"converted\" from transient to singleton because of that");
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
}