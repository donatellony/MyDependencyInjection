namespace MyDependencyInjection.Library
{
    public class ServiceProvider
    {
        // Lazy because we want to create the singleton after the other types have been already registered
        private readonly Dictionary<Type, Lazy<object>> _singletons = new();
        private readonly Dictionary<Type, Lazy<object>> _scopedInstances = new();
        private readonly Dictionary<Type, Func<object>> _transients = new();
        private readonly ServiceCollection _serviceCollection;

        public ServiceProvider(ServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            RegisterTypes(_serviceCollection);
        }

        private ServiceProvider(IDictionary<Type, Lazy<object>> singletons,
            ServiceCollection serviceCollection)
        {
            foreach (var singleton in singletons)
            {
                _singletons.Add(singleton.Key, singleton.Value);
            }

            _serviceCollection = serviceCollection;
            var nonSingletonServiceCollection =
                serviceCollection
                    .Where(desc => desc.Lifetime != ServiceLifetime.Singleton)
                    .ToList();
            RegisterTypes(nonSingletonServiceCollection);
        }


        public TService? GetService<TService>()
        {
            return (TService?)GetService(typeof(TService));
        }

        public ServiceScope CreateScope()
        {
            return new ServiceScope(this);
        }

        private object? GetService(Type serviceType)
        {
            if (TryGetScoped(serviceType, out var scoped)) return scoped;
            if (TryGetSingleton(serviceType, out var singleton)) return singleton;
            if (TryGetTransient(serviceType, out var transient)) return transient;

            return null;
        }

        private bool TryGetScoped(Type serviceType, out object? value)
        {
            if (!_scopedInstances.TryGetValue(serviceType, out var scoped))
            {
                value = null;
                return false;
            }

            value = scoped.Value;
            return true;
        }

        private bool TryGetTransient(Type serviceType, out object? value)
        {
            if (!_transients.TryGetValue(serviceType, out var transient))
            {
                value = null;
                return false;
            }

            value = transient.Invoke();
            return true;
        }

        private bool TryGetSingleton(Type serviceType, out object? value)
        {
            if (!_singletons.TryGetValue(serviceType, out var singleton))
            {
                value = null;
                return false;
            }

            value = singleton.Value;
            return true;
        }

        private ServiceProvider RegisterTypes(IEnumerable<ServiceDescriptor> serviceCollection)
        {
            foreach (var serviceDescriptor in serviceCollection)
            {
                switch (serviceDescriptor.Lifetime)
                {
                    case ServiceLifetime.Transient:
                        RegisterTransient(serviceDescriptor);
                        continue;
                    case ServiceLifetime.Singleton:
                        RegisterSingleton(serviceDescriptor);
                        continue;
                    case ServiceLifetime.Scoped:
                        RegisterScoped(serviceDescriptor);
                        break;
                }
            }

            return this;
        }

        private ServiceProvider RegisterTransient(ServiceDescriptor serviceDescriptor)
        {
            _transients[serviceDescriptor.ServiceType] = () => CreateService(serviceDescriptor);
            return this;
        }

        private ServiceProvider RegisterSingleton(ServiceDescriptor serviceDescriptor)
        {
            _singletons[serviceDescriptor.ServiceType] = new Lazy<object>(() => CreateService(serviceDescriptor));
            return this;
        }

        private ServiceProvider RegisterScoped(ServiceDescriptor serviceDescriptor)
        {
            _scopedInstances[serviceDescriptor.ServiceType] = new Lazy<object>(() => CreateService(serviceDescriptor));
            return this;
        }


        private object?[] GetConstructorParameters(ServiceDescriptor serviceDescriptor)
        {
            var constructorInfo = serviceDescriptor.ImplementationType.GetConstructors()
                .OrderByDescending(ctor => ctor.GetParameters().Any()).First();
            var parameters = constructorInfo.GetParameters()
                .Select(constructorParam => GetService(constructorParam.ParameterType))
                .ToArray();

            return parameters;
        }

        private object CreateService(ServiceDescriptor serviceDescriptor)
        {
            return Activator.CreateInstance(serviceDescriptor.ImplementationType,
                GetConstructorParameters(serviceDescriptor))!;
        }

        internal ServiceProvider GetScopedClone()
        {
            return new ServiceProvider(_singletons, _serviceCollection);
        }
    }
}