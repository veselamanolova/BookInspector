
namespace BookInspector.App.Providers
{
    using System;
    using BookInspector.App.Contracts;

    public sealed class CommandProcessor : ICommandProcessor
    {
        private ICommandParser parser;

        public CommandProcessor(ICommandParser parser)
        {
            this.parser = parser;
        }
        public string ProcessCommand(string inputLine)
        {
            throw new NotImplementedException();
        }
    }
}
