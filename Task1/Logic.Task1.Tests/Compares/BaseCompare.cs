namespace Logic.Task1.Tests.Compares
{
    public abstract class BaseCompare : ICompare<int[]>
    {
        public int Compare(int[] left, int[] right)
        {
            if (left == null && right == null)
            {
                return 0;
            }

            if (left == null)
            {
                return -1;
            }

            if (right == null)
            {
                return 1;
            }

            return GetCompareResult(left, right);
        }

        internal abstract int GetCompareResult(int[] left, int[] right);
    }
}
