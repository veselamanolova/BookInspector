
namespace BookInspector.App.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string inputLine);
    }
}
