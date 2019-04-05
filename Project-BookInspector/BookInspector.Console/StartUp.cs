
using System.Linq;
using System;
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

            var list = context.Category.ToList();

            foreach (var category in list)
                System.Console.WriteLine(category.Name);

            context.Dispose();
        }
    }
}

