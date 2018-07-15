using System;
using System.Collections.Generic;

namespace Logic.Task2
{
    public static class ICollectionExtension
    {
        public static T FindFirst<T>(this ICollection<T> collection, T value) where T: class
        {
            if (value == null)
            {
                throw new ArgumentNullException("T value can't be null!");
            }

            if (collection == null)
            {
                    throw new ArgumentNullException("Collection can't be null!");
            }

            foreach(var element in collection)
            {
                if (element.Equals(value))
                {
                    return element;
                }
            }

            return null;
        }

        public static T GetLast<T>(this ICollection<T> collection) where T : class
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection can't be null!");
            }

            T result = null;

            foreach (var element in collection)
            {
                result = element;
            }

            return result;
        }
    }
}
