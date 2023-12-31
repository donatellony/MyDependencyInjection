using Microsoft.Extensions.DependencyInjection;
using MyDependencyInjection.Shared;
using MyDependencyInjection.Shared.Services;

namespace MicrosoftDependencyInjection.Demo;

public sealed class ScopedDemo : IDemo
{
    private const int ScopesToSimulateAmount = 2;
    public void Run()
    {
        Console.WriteLine($"Start {nameof(MicrosoftDependencyInjection)}");
        RunTransientDemo();
        RunScopedDemo();
        RunSingletonDemo();
        Console.WriteLine($"End {nameof(MicrosoftDependencyInjection)}");
    }

    private static void RunScopedDemo()
    {
        Console.WriteLine(nameof(RunScopedDemo));
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IDateTimeProvider, DateTimeProvider>();
        RunSharedDemo(serviceCollection);
    }

    private static void RunSingletonDemo()
    {
        Console.WriteLine(nameof(RunSingletonDemo));
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        RunSharedDemo(serviceCollection);
    }
    
    private static void RunTransientDemo()
    {
        Console.WriteLine(nameof(RunTransientDemo));
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IDateTimeProvider, DateTimeProvider>();
        RunSharedDemo(serviceCollection);
    }

    private static void RunSharedDemo(ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IGuidGenerator, GuidGenerator>();
        serviceCollection.AddTransient<IConsoleWriter, ConsoleWriter>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        for (int i = 0; i < ScopesToSimulateAmount; i++)
        {
            using var scope = serviceProvider.CreateScope();
            var consoleWriter1 = scope.ServiceProvider.GetService<IConsoleWriter>();
            var consoleWriter2 = scope.ServiceProvider.GetService<IConsoleWriter>();
            consoleWriter1?.WriteSomeSpecificInfo();
            consoleWriter2?.WriteSomeSpecificInfo();
        }
    }
}