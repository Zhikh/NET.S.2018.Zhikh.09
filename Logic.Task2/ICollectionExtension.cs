using System;
using System.Collections.Generic;

namespace Logic.Task2
{
    public static class ICollectionExtension
    {
        public static T FindFirst<T>(this ICollection<T> collection, T value) where T: class
        {
            foreach(var element in collection)
            {
                if (element.Equals(value))
                {
                    return element;
                }
            }

            return null;
        }
    }
}
