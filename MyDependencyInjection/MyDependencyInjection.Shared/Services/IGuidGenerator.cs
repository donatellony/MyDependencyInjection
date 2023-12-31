namespace MyDependencyInjection.Shared.Services
{
    public interface IGuidGenerator
    {
        Guid Guid { get; }
        string GetGuidAndNowTicks();
    }
}