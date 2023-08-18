namespace MyDependencyInjectionLibrary
{
    public class ServiceCollection
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors = new();

        public ServiceCollection AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _serviceDescriptors.Add(
                new ServiceDescriptor()
                {

                }
            );
        }
    }
}
