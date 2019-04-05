
using System.Linq;
using BookInspector.Data.Context;

namespace BookInspector.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // Command Line Interface
            //new Builder().AppBuilder();

            var context = new BookInspectorContext();

            var list = context.Category.ToList();

            foreach (var category in list)
                System.Console.WriteLine(category.Name);

            context.Dispose();
        }
    }
}

