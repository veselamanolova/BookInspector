
namespace BookInspector.App.Providers
{
    using Autofac;
    using BookInspector.App.Contracts;

    public class CommandParser : ICommandParser
    {
        private IComponentContext _commandContext;

        public CommandParser(IComponentContext command)
        {
            _commandContext = command;
        }

        public ICommand ParseCommand(string commandName)
        {
            return _commandContext.ResolveNamed<ICommand>(commandName.ToLower());
        }
    }
}
