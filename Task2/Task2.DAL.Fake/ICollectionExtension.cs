using System;
using System.Collections.Generic;
using Task2.DAL.Interface.DTO;

namespace DAL.Task2
{
    public static class ICollectionExtension
    {
        public static T FindFirst<T>(this ICollection<T> collection, T value) where T : IEntity
        {
            if (value == null)
            {
                throw new ArgumentNullException("T value can't be null!");
            }

            if (collection == null)
            {
                    throw new ArgumentNullException("Collection can't be null!");
            }

            foreach (var element in collection)
            {
                if (element.Equals(value))
                {
                    return element;
                }
            }

            return default(T);
        }

        public static T GetLast<T>(this ICollection<T> collection) where T : IEntity
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection can't be null!");
            }

            T result = default(T);

            foreach (var element in collection)
            {
                result = element;
            }

            return result;
        }
    }
}
