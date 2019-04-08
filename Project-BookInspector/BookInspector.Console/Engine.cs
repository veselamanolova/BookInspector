
namespace BookInspector.App
{
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;
    using System;

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
                _writer.WriteLine($"\nLast command: {_command} \nResult: {_executeResult}");
                _command = Menu.Choice();

                try
                {
                    _executeResult = processor.ProcessCommand(_command.Replace(" ", ""));
                }
                catch (ArgumentException e)
                {
                    _executeResult = e.Message; 
                }
                
                _writer.Clear();
            }
        }
    }
}

