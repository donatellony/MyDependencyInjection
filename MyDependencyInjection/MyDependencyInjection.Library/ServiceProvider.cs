namespace MyDependencyInjection.Library
{
    public class ServiceProvider
    {
        // Lazy because we want to create the singleton after the other types have been already registered
        private readonly Dictionary<Type, Lazy<object>> _singletons = new();
        private readonly Dictionary<Type, Lazy<object>> _scopedInstances = new();
        private readonly Dictionary<Type, Func<object>> _transients = new();

        public ServiceProvider(ServiceCollection serviceCollection)
        {
            RegisterTypes(serviceCollection);
        }

        public TService? GetService<TService>()
        {
            return (TService?)GetService(typeof(TService));
        }

        private object? GetService(Type serviceType)
        {
            if (TryGetSingleton(serviceType, out var valueValue)) return valueValue;

            return _transients[serviceType]?.Invoke();
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

        private ServiceProvider RegisterTypes(ServiceCollection serviceCollection)
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
    }
}