namespace MyDependencyInjection.Library
{
    public class ServiceCollection : List<ServiceDescriptor>
    {
        public ServiceCollection AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return AddDescriptor<TService, TImplementation>(ServiceLifetime.Singleton);
        }

        public ServiceCollection AddSingleton<TImplementation>()
            where TImplementation : class
        {
            return AddDescriptor<TImplementation, TImplementation>(ServiceLifetime.Singleton);
        }
        public ServiceCollection AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return AddDescriptor<TService, TImplementation>(ServiceLifetime.Transient);
        }

        public ServiceCollection AddTransient<TImplementation>()
            where TImplementation : class
        {
            return AddDescriptor<TImplementation, TImplementation>(ServiceLifetime.Transient);
        }

        private ServiceCollection AddDescriptor<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            Add(
                new ServiceDescriptor()
                {
                    ServiceType = typeof(TService),
                    ImplementationType = typeof(TImplementation),
                    Lifetime = lifetime
                }
            );
            return this;
        }

        public ServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(this);
        }
    }
}
