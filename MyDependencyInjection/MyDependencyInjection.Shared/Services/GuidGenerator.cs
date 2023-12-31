namespace MyDependencyInjection.Shared.Services
{
    /// <summary>
    /// Depends on DateTimeProvider
    /// </summary>
    public class GuidGenerator : IGuidGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public GuidGenerator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public Guid Guid { get; } = Guid.NewGuid();
        public string GetGuidAndNowTicks()
        {
            return $"{Guid}, current time in ticks: {_dateTimeProvider.Now.Ticks}";
        }
    }
}
