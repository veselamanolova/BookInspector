
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
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }

        public static void IfNotExist<T>(string s, string ex) where T : Exception, new()
        {
            if (!_context.User.Any(u => u.Name == s))
                throw (T)Activator.CreateInstance(typeof(T), ex);
        }
    }
}

