
namespace BookInspector.App.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandName);
    }
}
