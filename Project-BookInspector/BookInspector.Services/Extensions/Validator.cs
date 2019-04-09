﻿using System;
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Context;

    public static class Validator
    {
        private static readonly BookInspectorContext _context = new BookInspectorContext();

        public static void IfNullOrEmpty<T>(object o) where T : Exception, new()
        {
            if (o is null || o.Equals(string.Empty))
                throw new T();
        }

        public static void IfIsNotInRange<T>(string s) where T : Exception, new()
        {
            if (s.Length < 2 || s.Length >= 50)
                throw new T();
        }

        public static void IfExist<T>(string s, string ex) where T : Exception, new()
        {
            if (_context.User.Any(u => u.Name == s))
                throw new ArgumentException(ex);
        }

        public static void IfNotExist<T>(string s, string ex) where T : Exception, new()
        {
            if (!_context.User.Any(u => u.Name == s))
                throw new ArgumentException(ex);
        }

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