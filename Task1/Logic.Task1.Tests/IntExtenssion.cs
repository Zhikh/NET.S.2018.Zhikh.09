namespace Logic.Task1.Tests
{
    internal static class IntExtenssion
    {
        internal static int GetMax(this int[] array)
        {
            int i = 0;
            int result = array[i++];

            for (; i < array.Length; i++)
            {
                if (array[i] > result)
                {
                    result = array[i];
                }
            }

            return result;
        }

        internal static int GetMin(this int[] array)
        {
            int i = 0;
            int result = array[i++];

            for (; i < array.Length; i++)
            {
                if (array[i] < result)
                {
                    result = array[i];
                }
            }

            return result;
        }

        internal static int GetSum(this int[] array)
        {
            int result = 0;

            foreach (var element in array)
            {
                result += element;
            }

            return result;
        }
    }
}
