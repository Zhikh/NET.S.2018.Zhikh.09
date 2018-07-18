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
        /// <param name="isAscending"> Type of sorting: Ascending/Decreasing </param>
        public static void Sort<T>(T[] array, ICompare<T> compare, bool isAscending = true)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"Argument {nameof(array)} can't be null!");
            }

            if (compare == null)
            {
                throw new ArgumentNullException($"Argument {nameof(compare)} can't be null!");
            }

            int n = array.Length;
            bool isSwap = true;
            for (int i = 0; i < n && isSwap; i++)
            {
                isSwap = false;
                for (int j = 0; j < n - 1; j++)
                {
                    if (compare.Compare(array[j + 1], array[j]) < 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        isSwap = true;
                    }
                }
            }

            if (!isAscending)
            {
                Reverse(array);
            }
        }
        #endregion

        #region Private methods
        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;

            first = second;
            second = temp;
        }

        private static void Reverse<T>(T[] array)
        {
            int n = array.Length;
            T[] reversedArray = new T[n];

            int j = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                reversedArray[j++] = array[i];
            }

            for (int i = 0; i < n; i++)
            {
                array[i] = reversedArray[i];
            }
        }
        #endregion
    }
}
