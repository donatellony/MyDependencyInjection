namespace MyDependencyInjection.Shared.Services
{
    /// <summary>
    /// Doesn't depend on anything
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; } = DateTime.Now;
    }
}