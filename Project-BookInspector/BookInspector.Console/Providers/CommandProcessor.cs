
namespace BookInspector.App.Providers
{
    using System.Collections.Generic;
    using BookInspector.App.Contracts;

    public sealed class CommandProcessor : ICommandProcessor
    {
        private IReader _reader;
        private IWriter _writer;
        private ICommandParser _parser;
        private IReadOnlyList<string> _args;

        public CommandProcessor(ICommandParser parser, IReader reader, IWriter writer)
        {
            _parser = parser;
            _reader = reader;
            _writer = writer;
        }

        public string ProcessCommand(string commandName)
        {
            var command = _parser.ParseCommand(commandName.ToLower());
            _writer.WriteLine("\n");
            _args = _reader.ReadLine().Split('*');
            return command.Execute(_args);
        }
    }
}

