
namespace BookInspector.App.Providers
{
    using System;

    public class Menu
    {
        public static string Choice()
        {
            const int startX = 0;
            const int startY = 4;
            const int optionsPerLine = 1;

            string[] options =
            {
                "Add User",
                "Add Book",
                "Add Author",
                "Add Publisher",
                "Add Category",
                "Show all Users",
                "Show all Books",
                "Show all Categories",
                "Find User"
            };

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine),
                        startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (currentSelection >= optionsPerLine)
                            currentSelection -= optionsPerLine;
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        if (currentSelection + optionsPerLine < options.Length)
                            currentSelection += optionsPerLine;
                        break;
                    }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            Console.WriteLine("\n");

            return options[currentSelection];
        }
    }
}

