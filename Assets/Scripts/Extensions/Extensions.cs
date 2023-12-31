using System.Collections.Generic;

namespace ShatterShapes.Extensions
{
    public static class Extensions
    {
        public static void AddSafe<TK,TV> (this Dictionary<TK,TV> dictionary, TK key, TV value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void AddSafe<T>(this List<T> list, T obj)
        {
            if (!list.Contains(obj))
            {
                list.Add(obj);
            }
        }
    }
}
