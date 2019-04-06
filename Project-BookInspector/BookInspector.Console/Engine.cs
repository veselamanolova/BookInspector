
namespace BookInspector.App
{
    using System;
    using BookInspector.App.Contracts;

    class Engine : IRun
    {
        private IReader reader;
        private IWriter writer;
        private ICommandProcessor processor;

        public Engine(IReader reader, IWriter writer, ICommandProcessor processor)
        {
            this.processor = processor;
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var commandLine = reader.ReadLine();
                    var result = processor.ProcessCommand(commandLine);
                    writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    writer.WriteLine(e.Message);
                }
            }
        }
    }
}
