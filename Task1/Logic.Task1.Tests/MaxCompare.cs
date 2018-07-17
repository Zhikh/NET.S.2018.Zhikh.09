using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task1.Tests
{
    public class MaxCompare : ICompare<int[]>
    {
        public int Compare(int[] left, int[] right)
        {
            if (left == null && right == null)
                return 0;
            if (left == null)
                return -1;
            if (right == null)
                return 1;
            if (left[]> predicat.GetValue(right))
            {
                return -1;
            }
            if (predicat.GetValue(left) < predicat.GetValue(right))
            {
                return 1;
            }
            return 0;
        }
    }
}
