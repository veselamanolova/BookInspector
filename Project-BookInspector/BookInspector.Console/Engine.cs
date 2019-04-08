
namespace BookInspector.App
{
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;

    class Engine : IRun
    {
        private IReader _reader;
        private IWriter _writer;
        private ICommandProcessor processor;

        private string _command;
        private string _executeResult;

        public Engine(IReader reader, IWriter writer, ICommandProcessor processor)
        {
            this.processor = processor;
            _reader = reader;
            _writer = writer;
        }

        public void Run()
        {
            while (true)
            {
                _writer.WriteLine($"\nLast command: {_command} \nResult: {_executeResult}");
                _command = Menu.Choice();
                _executeResult = processor.ProcessCommand(_command.Replace(" ", ""));
                _writer.Clear();
            }
        }
    }
}

