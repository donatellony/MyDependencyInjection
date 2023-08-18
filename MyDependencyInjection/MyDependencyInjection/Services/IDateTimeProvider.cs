namespace MyDependencyInjectionConsoleApp.Services
{
    // Interface is created to make the DateTime.Now mockable
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}