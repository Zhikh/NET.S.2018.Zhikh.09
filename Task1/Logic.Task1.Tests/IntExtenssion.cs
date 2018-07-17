using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task1.Tests
{
    public static class IntExtenssion
    {
        public static int GetMax(this int[] array)
        {
            int result = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > result)
                {
                    result = array[i];
                }
            }

            return result;
        }

        public static int GetMin(this int[] array)
        {
            int result = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < result)
                {
                    result = array[i];
                }
            }

            return result;
        }
    }
}
