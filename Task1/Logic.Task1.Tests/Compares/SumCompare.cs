namespace Logic.Task1.Tests.Compares
{
    public sealed class SumCompare : BaseCompare, ICompare<int[]>
    {
        internal override int GetCompareResult(int[] left, int[] right)
        {
            int leftSum = left.GetSum();
            int rightSum = right.GetSum();

            if (leftSum > rightSum)
            {
                return 1;
            }

            if (leftSum < rightSum)
            {
                return -1;
            }

            return 0;
        }
    }
}
