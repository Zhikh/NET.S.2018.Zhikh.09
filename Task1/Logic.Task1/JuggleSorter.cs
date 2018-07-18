using System;

namespace Logic.Task1
{
    public static class JuggleSort
    {
        #region Public methods
        /// <summary>
        /// Sorts (ascending/decreasing) juggle array by min, max or sum value of subarrays
        /// </summary>
        /// <param name="array"> Array for sorting </param>
        /// <param name="mode"> Ascending/decreasing </param>
        /// <param name="value"> Min/max/sum </param>
        public static void Sort(int[][] array, ICompare<int[]> compare)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            Sorter.Sort(array, compare);
        }
        #endregion
    }
}
