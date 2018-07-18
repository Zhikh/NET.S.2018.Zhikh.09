namespace Logic.Task1.Tests.Compares
{
    public sealed class MinCompare : BaseCompare, ICompare<int[]> 
    {
        internal override int GetCompareResult(int[] left, int[] right)
        {
            int leftMin = left.GetMin();
            int rightMin = right.GetMin();

            if (leftMin > rightMin)
            {
                return 1;
            }

            if (leftMin < rightMin)
            {
                return -1;
            }

            return 0;
        }
    }
}
