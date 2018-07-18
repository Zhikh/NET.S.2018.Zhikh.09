using System;

namespace Logic.Task1
{
    public static class Sorter
    {
        public static void Sort<T>(T[] array, ICompare<T> compare)
        {
            if (array == null)
            {
                throw new ArgumentNullException("Argument array can't be null!");
            }

            int n = array.Length;
            bool IsSwap = true;
            for (int i = 0; i < n && IsSwap; i++)
            {
                IsSwap = false;
                for (int j = 0; j < n - 1; j++)
                {
                    if (compare.Compare(array[j + 1], array[j]) < 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        IsSwap = true;
                    }
                }
            }
        }

        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;

            first = second;
            second = temp;
        }
    }
}
