namespace MyDependencyInjection.Library;

public class ServiceScope : IDisposable
{
    public readonly ServiceProvider ServiceProvider;

    internal ServiceScope(ServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider.GetScopedClone();
    }

    public void Dispose()
    {
    }
}