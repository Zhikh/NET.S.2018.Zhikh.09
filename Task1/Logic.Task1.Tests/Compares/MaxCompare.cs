namespace Logic.Task1.Tests.Compares
{
    public sealed class MaxCompare : BaseCompare, ICompare<int[]>
    {
        internal override int GetCompareResult(int[] left, int[] right)
        {
            int leftMax = left.GetMax();
            int rightMax = right.GetMax();

            if (leftMax > rightMax)
            {
                return -1;
            }

            if (leftMax < rightMax)
            {
                return 1;
            }

            return 0;
        }
    }
}
