namespace MyDependencyInjectionConsoleApp.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; } = DateTime.Now;
    }
}