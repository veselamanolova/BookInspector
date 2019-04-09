
namespace BookInspector.App
{
    using System;
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;

    class Engine : IRun
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;
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
                _writer.Clear();
                _writer.WriteLine(new string('\n', 18));
                _writer.WriteLine($"\nLast command: {_command} \nResult: {_executeResult}");
                _command = Menu.Choice();

                try
                {
                    _executeResult = processor.ProcessCommand(_command.Replace(" ", ""));
                }

                catch (ArgumentException ex)
                {
                    _executeResult = $"ERROR: {ex.Message}"; 
                }
            }
        }
    }
}

