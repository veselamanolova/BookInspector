
namespace BookInspector.App.Providers
{
    using System.Collections.Generic;
    using BookInspector.App.Contracts;

    public sealed class CommandProcessor : ICommandProcessor
    {
        private IReader _reader;
        private ICommandParser _parser;
        private IReadOnlyList<string> _args;

        public CommandProcessor(ICommandParser parser, IReader reader)
        {
            _parser = parser;
            _reader = reader;
        }

        public string ProcessCommand(string commandName)
        {
            var command = _parser.ParseCommand(commandName.ToLower());
            _args = _reader.ReadLine().Split('*');
            return command.Execute(_args);
        }
    }
}

