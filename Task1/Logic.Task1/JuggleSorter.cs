using System;

namespace Logic.Task1
{
    // TODO: ICompare { CompareTo } 
    public static class JuggleSort
    {
        /// <summary>
        /// Type of sorting
        /// </summary>
        public enum Mode
        {
            Ascending,
            Decreasing
        }

        /// <summary>
        /// Type of value for sorting order of subarrays in juggle array
        /// </summary>
        public enum Value
        {
            None,
            Min,
            Max,
            Sum
        }

        #region Public methods
        /// <summary>
        /// Sorts (ascending/decreasing) juggle array by min, max or sum value of subarrays
        /// </summary>
        /// <param name="array"> Array for sorting </param>
        /// <param name="mode"> Ascending/decreasing </param>
        /// <param name="value"> Min/max/sum </param>
        // Sort(int[][], IComparer)
        public static void Sort(int[][] array, Mode mode = Mode.Ascending, Value value = Value.None)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            SortSubarrays(array);
            
            switch (value)
            {
                case Value.Min:
                    array.SortByValue(GetMin);
                    break;
                case Value.Max:
                    array.SortByValue(GetMax);
                    break;
                case Value.Sum:
                    array.SortByValue(GetSum);
                    break;
            }

            if (mode == Mode.Decreasing)
            {
                array.Reverse();
            }
        }
        #endregion

        #region Private methods
        #region Methods for sorting
        private static void SortByValue(this int[][] array, Func<int[], int> getValue)
        {
            int n = array.Length;
            int[] values = new int[n];

            for (int i = 0;  i < n; i++)
            {
                values[i] = getValue(array[i]);
            }

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

        #region Get values (min, max, sum)
        private static int GetSum(int[] array)
        {
            int result = 0;

            foreach (var value in array)
            {
                result += value;
            }

            return result;
        }

        private static int GetMin(int[] array)
        {
            return array[0];
        }

        private static int GetMax(int[] array)
        {
            return array[array.Length - 1];
        }
        #endregion

        #region Additional methods
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
        #endregion
        #endregion
    }
}
