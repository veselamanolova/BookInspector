
namespace BookInspector.Services
{
    using System;

    public static class Validator
    {
        public static void IfNull<TSource>(object o) where TSource : Exception, new()
        {
            if (o is null) throw new TSource();
        }

        public static void IfNull<TSource>(object o, string ex) where TSource : Exception, new()
        {
            if (o is null)
                throw (TSource)Activator.CreateInstance(typeof(TSource), ex);
        }

        public static void IfNotNull<TSource>(object o, string ex) where TSource : Exception, new()
        {
            if (o != null)
                throw (TSource)Activator.CreateInstance(typeof(TSource), ex);
        }

        public static void IfIsNotInRange<TSource>(string s) where TSource : Exception, new()
        {
            if (s.Length < 2 || s.Length >= 50)
                throw new TSource();
        }

        public static void IsInRange<TSource>(int value, int min, int max, string ex) where TSource : Exception, new()
        {
            if (value < min || value > max)
                throw (TSource)Activator.CreateInstance(typeof(TSource), ex);
        }

        public static void CheckExactLength<TSource>(string value, int exact, string ex) where TSource : Exception, new()
        {
            if (value.Length != exact)
                throw (TSource)Activator.CreateInstance(typeof(TSource), ex);
        }
    }
}

