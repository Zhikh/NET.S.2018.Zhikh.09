using System;

namespace Logic.Task1
{
    public static class JuggleSort
    {
        public enum Mode { Ascending, Decreasing }
        public enum Value { None, Min, Max, Sum}

        #region Public methods
        public static void Sort(int[][] array, Mode mode = Mode.Ascending, Value value = Value.None)
        {
            SortSubarrays(array);
            
            switch (value)
            {
                case Value.Min:
                    array.SortByMin();
                    break;
                case Value.Max:
                    array.SortByMax();
                    break;
                case Value.Sum:
                    array.SortBySum();
                    break;
            }

            if (mode == Mode.Decreasing)
            {
                array.Reverse();
            }
        }
        #endregion

        #region Private methods
        private static void SortByMin(this int[][] array)
        {
            
        }
        
        private static void SortByMax(this int[][] array)
        {
           
        }

        private static void SortBySum(this int[][] array)
        {
            int n = array.Length;
            int[] sums = new int[n];

            for (int i = 0;  i < n; i++)
            {
                sums[i] = GetSum(array[i]);
            }

            SortByValue(array, sums);
        }

        private static int GetSum(int[] array)
        {
            int result = 0;

            foreach (var value in array)
            {
                result += value;
            }

            return result;
        }

        private static void Reverse(this int[][] array)
        {
            int n = array.Length;
            int[][] reversedArray = new int[n][];

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

        private static void SortByValue(int[][] array, int[] values)
        {
            int n = values.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (values[j + 1] < values[j])
                    {
                        Swap(ref values[j], ref values[j + 1]);
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

        private static void SortSubarrays<T>(T[][] array) where T : IComparable<T>
        {
            foreach (var element in array)
            {
                Sorter.Sort(element);
            }
        }
        #endregion
    }
}
