using System;

namespace Logic.Task1
{
    public static class Sorter
    {
        public enum Mode { Ascending, Decreasing }

        public static void Sort<T>(T[] array, Mode mode = Mode.Ascending) where T : IComparable<T>
        {
            if (array == null)
            {
                throw new ArgumentNullException("Argument array can't be null!");
            }

            int n = array.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (array[j + 1].CompareTo(array[j]) < 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
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
