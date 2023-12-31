// TODO inject demos with MyDependencyInjection too
ScopedDemos();
return;

void ScopedDemos()
{
    var microsoftScopedDemo = new MicrosoftDependencyInjection.Demo.ScopedDemo();
    var myScopedDemo = new MyDependencyInjection.Demo.ScopedDemo();

    Console.WriteLine("These demos' code is identical.\n The First one is made with Microsoft's DI implementation,\n The Second one - with mine.");
    Console.WriteLine("In both, I inject the DateTimeProvider in all of possible Lifetimes.");
    Console.WriteLine("In both, all of the services are resolved only inside of a Scope.");
    Console.WriteLine("With that in mind,");
    Console.WriteLine("- Transient should make all the ticks differ from each other.");
    Console.WriteLine("- Scoped should make the ticks IN DIFFERENT SCOPES differ from each other.");
    Console.WriteLine("- Singleton should make the ticks same in every of the scopes.");
    WriteConsoleInfoSeparator();
    microsoftScopedDemo.Run();
    WriteConsoleInfoSeparator();
    myScopedDemo.Run();
    WriteConsoleInfoSeparator();
}

void WriteConsoleInfoSeparator()
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
}