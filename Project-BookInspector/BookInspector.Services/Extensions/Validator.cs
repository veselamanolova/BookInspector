
namespace BookInspector.Services
{
    using System;
    using System.Collections.Generic;
    using BookInspector.Data.Context;

    public static class Validator
    {
        private static readonly BookInspectorContext _context = new BookInspectorContext();

        public static void IfNull<T>(object o) where T : Exception, new()
        {
            if (o is null) throw new T();
        }

        public static void IfIsNotInRange<T>(string s) where T : Exception, new()
        {
            if (s.Length < 2 || s.Length >= 50)
                throw new T();
        }

        public static void IfExist<T>(List<string> list, string s, string ex) where T : Exception, new()
        {
            if (list.Contains(s))
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }

        public static void IfNotExist<T>(List<string> list, string s, string ex) where T : Exception, new()
        {
            if (!list.Contains(s))
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }

        public static void IsInRange<T>(int value, int min, int max, string ex) where T : Exception, new()
        {
            if (value < min || value > max)
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }

        public static void CheckExactLength<T>(string value, int exact, string ex) where T : Exception, new()
        {
            if (value.Length != exact)
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }
    }
}

