
namespace BookInspector.Services
{
    using System;

    public static class Validation
    {
        public static void IsInRange(this string str, int min, int max)
        {
            if (str.Length < min || str.Length > max)
            {
                throw new ArgumentException($"The string length should be between {min} and {max}");
            }
        }

        public static void IsInRange(this int value, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException($"Allowed valuse are between {min} and {max}");
            }
        }

    }
}