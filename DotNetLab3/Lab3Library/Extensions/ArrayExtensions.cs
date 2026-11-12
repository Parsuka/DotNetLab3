using System;
using System.Collections.Generic;

namespace Lab3Library.Extensions
{
    public static class ArrayExtensions
    {
        public static int CountOf<T>(this T[] array, T value) where T : IEquatable<T>
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int count = 0;
            foreach (T item in array)
            {
                if (AreEqual(item, value))
                    count++;
            }
            return count;
        }

        public static T[] Unique<T>(this T[] array) where T : IEquatable<T>
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new List<T>(array.Length);
            foreach (T item in array)
            {
                bool alreadyAdded = false;
                foreach (T added in result)
                {
                    if (AreEqual(added, item))
                    {
                        alreadyAdded = true;
                        break;
                    }
                }
                if (!alreadyAdded)
                    result.Add(item);
            }
            return result.ToArray();
        }

        private static bool AreEqual<T>(T first, T second) where T : IEquatable<T>
        {
            if (first == null)
                return second == null;
            return first.Equals(second);
        }
    }
}
