namespace Logic.Task1.Tests.Compares
{
    public sealed class MinCompare : BaseCompare, ICompare<int[]> 
    {
        /// <summary>
        /// Set type of comparing
        /// </summary>
        /// <param name="isLess"></param>
        public MinCompare(bool isAscending = true) : base(isAscending)
        {
        }

        /// <summary>
        /// Compare int[] elements
        /// </summary>
        /// <param name="left"> int[] value for comparing </param>
        /// <param name="right"> int[] value for comparing  </param>
        /// <returns> Result of comparing (left == right - 0, > - -1, else 1 </returns>
        internal override int GetCompareResult(int[] left, int[] right)
        {
            int leftMin = left.GetMin();
            int rightMin = right.GetMin();

            if (leftMin > rightMin)
            {
                return _comparingResult;
            }

            if (leftMin < rightMin)
            {
                return -1 * _comparingResult;
            }

            return 0;
        }
    }
}
