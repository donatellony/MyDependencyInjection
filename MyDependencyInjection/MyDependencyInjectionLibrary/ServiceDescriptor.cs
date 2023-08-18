namespace MyDependencyInjectionLibrary
{
    public class ServiceDescriptor
    {
        public Type Service { get; init; }
        public Type Implementation { get; init; }
        public ServiceLifetime Lifetime { get; init; }
    }
}
