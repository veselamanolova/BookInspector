
namespace BookInspector.App.Providers
{
    using System;

    public class Menu
    {
        public static string Choice()
        {
            const int startX = 1;
            const int startY = 1;
            const int optionsPerLine = 1;
            const int spacingPerLine = 14;

            string[] options =
            {
                "Add User",
                "Add Book",
                "Add Category",
                "Show all Users",
                "Show all Books",
                "Show all Categories"
            };

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine,
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

            return options[currentSelection];
        }
    }
}

