namespace Logic.Task1.Tests.Compares
{
    public abstract class BaseComparer : IComparer<int[]>
    {
        protected readonly int _comparingResult;

        /// <summary>
        /// Set type of comparing
        /// </summary>
        /// <param name="isLess"></param>
        public BaseComparer(bool isLess = true)
        {
            _comparingResult = isLess ? 1 : -1;
        }

        /// <summary>
        /// Compare int[] elements
        /// </summary>
        /// <param name="left"> int[] value for comparing </param>
        /// <param name="right"> int[] value for comparing  </param>
        /// <returns> Result of comparing (left == right - 0, > - -1, else 1 </returns>
        public int Compare(int[] left, int[] right)
        {
            if (left == null && right == null)
            {
                return 0;
            }

            if (left == null)
            {
                return -1 * _comparingResult;
            }

            if (right == null)
            {
                return _comparingResult;
            }

            return GetCompareResult(left, right);
        }

        internal abstract int GetCompareResult(int[] left, int[] right);
    }
}
