namespace MyDependencyInjection.Library
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; init; }
        public Type ImplementationType { get; init; }
        public ServiceLifetime Lifetime { get; init; }
    }
}
