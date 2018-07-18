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
        public static void Sort<T>(T[] array, ICompare<T> compare)
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
        #endregion

        #region Private methods
        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;

            first = second;
            second = temp;
        }
        #endregion
    }
}
