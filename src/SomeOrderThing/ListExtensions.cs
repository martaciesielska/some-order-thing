using System.Collections.Generic;

namespace SomeOrderThing
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }

        public static void AddRange<T>(this List<T> list, T[] items1, params T[] items2)
        {
            list.AddRange(items1);
            list.AddRange(items2);
        }
    }
}
