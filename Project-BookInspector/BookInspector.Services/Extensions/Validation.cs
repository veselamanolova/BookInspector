
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
    }
}