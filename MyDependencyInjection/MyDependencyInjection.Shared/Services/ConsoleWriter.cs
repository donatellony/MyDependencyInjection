namespace MyDependencyInjection.Shared.Services
{
    public class ConsoleWriter : IConsoleWriter
    {
        private readonly IGuidGenerator _guidGenerator;

        public ConsoleWriter(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteSomeSpecificInfo()
        {
            WriteLine(_guidGenerator.GetGuidAndNowTicks());
        }
    }
}