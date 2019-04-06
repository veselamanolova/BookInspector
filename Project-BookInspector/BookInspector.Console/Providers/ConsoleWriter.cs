
namespace BookInspector.App.Providers
{
    using BookInspector.App.Contracts;

    class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
