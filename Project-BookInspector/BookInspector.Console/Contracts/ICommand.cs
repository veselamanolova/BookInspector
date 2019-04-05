
namespace BookInspector.Console.Contracts
{
    using System.Collections.Generic;

    public interface ICommand
    {
        string Execute(IReadOnlyList<string> args);
    }
}

