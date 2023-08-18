namespace MyDependencyInjectionConsoleApp.Services
{
    // Interface is created to make the Console.Writeline mockable
    public interface IConsoleWriter
    {
        void WriteSomeSpecificInfo();
        void WriteLine(string text);
    }
}