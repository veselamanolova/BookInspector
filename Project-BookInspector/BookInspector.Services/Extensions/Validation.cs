
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
                throw new ArgumentException($"Allowed values are between {min} and {max}");
            }
        }

        public static void IsInRange(this int value, int min, int max, string stringType)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException($"Allowed values for {stringType} are between {min} and {max}");
            }
        }

        public static void CheckExactLength(this string value, int exact, string stringType)
        {
            if (value.Length != exact)
            {
                throw new ArgumentException($"{stringType} should be exactly {exact} characters!");
            }
        }
       



    }
}