namespace MyDependencyInjection.Library;

public class ServiceScope
{
    public readonly ServiceProvider ServiceProvider;
    private readonly ServiceProvider _originalServiceProvider;

    internal ServiceScope(ServiceProvider serviceProvider)
    {
        _originalServiceProvider = serviceProvider;
    }
}