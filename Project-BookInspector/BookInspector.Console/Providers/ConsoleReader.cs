
namespace BookInspector.App.Providers
{
    using BookInspector.App.Contracts;

    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}
