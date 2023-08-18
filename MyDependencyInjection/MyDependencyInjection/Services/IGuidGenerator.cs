namespace MyDependencyInjectionConsoleApp.Services
{
    public interface IGuidGenerator
    {
        Guid Guid { get; }
        string GetGuidAndNowTicks();
    }
}