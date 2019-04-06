
namespace BookInspector.App
{
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;

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
                string commandLine = Menu.Choice();
                var result = processor.ProcessCommand(commandLine.Replace(" ", ""));
                writer.WriteLine(result);
            }
        }
    }
}
