using System;

namespace Logic.Task1
{
    public static class Sorter
    {
        #region Public API
        /// <summary>
        /// Sorts array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"> Array for sorting </param>
        /// <param name="compare"> Rules for comparing of arrays elements </param> 
        /// <exception cref="ArgumentNullException"> When array is null or object of IComparer is null </exception>
        public static void Sort<T>(T[] array, IComparer<T> compare)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"Argument {nameof(array)} can't be null!");
            }

            if (compare == null)
            {
                throw new ArgumentNullException($"Argument {nameof(compare)} can't be null!");
            }

            bool isSwap;
            do
            {
                isSwap = false;

                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (compare.Compare(array[j + 1], array[j]) < 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        isSwap = true;
                    }
                }
            }
            while (isSwap);
        }

        /// <summary>
        /// Sorts array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"> Array for sorting </param>
        /// <param name="compare"> Method for comparing of arrays elements </param>
        /// <exception cref="ArgumentNullException"> When array is null </exception>
        public static void Sort<T>(T[] array, Comparison<T> compare)
        {
            Sort(array, new Nested<T>(compare));
        }
        #endregion

        #region Private methods
        private class Nested<T> : IComparer<T>
        {
            private Comparison<T> _compareMethod;

            public Nested(Comparison<T> compare)
            {
                _compareMethod = compare;
            }

            public int Compare(T left, T right)
            {
                return _compareMethod(left, right);
            }
        }

        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;

            first = second;
            second = temp;
        }
        #endregion
    }
}
