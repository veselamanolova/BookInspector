
using System.Linq;
using BookInspector.Console;
using BookInspector.Data.Context;

namespace BookInspector.CLI
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // Command Line Interface
            //new Builder().AppBuilder();

            var context = new BookInspectorContext();

            var list = context.Category.Select(x => x.Name).ToList();
            

            context.Dispose();
        }
    }
}

