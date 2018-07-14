using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Logic.Task1.Tests
{
    [TestFixture]
    public class JuggleSorterTests
    {
        Enum[] mods = new Enum[]
        {
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing
        };

        private int[][][] _sourceData = new int[][][]
        {
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                new int[] { 6, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            },
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                new int[] { 6, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            }
        };

        private int[][][] _resultSum = new int[][][]
        {
            new int[][]
            {
                new int[] { 1, 2, 6 },
                new int[] { 2, 3, 4, 6 },
                new int[] { 0, 1, 3, 6, 8, 9, 9 }
            },
            new int[][]
            {
                new int[] { 0, 1, 3, 6, 8, 9, 9 },
                new int[] { 2, 3, 4, 6 },
                new int[] { 1, 2, 6 }
            }
        };

        [Test]
        public void SortBySum_JuggleArrayAscending_SuccessfulTests()
        {
            for (int i = 0; i < _sourceData.Length; i++)
            {
                JuggleSort.Sort(_sourceData[i], value: JuggleSort.Value.Sum, mode: (JuggleSort.Mode)mods[i]);

                if (!MatrixAreEquals(_sourceData[i], _resultSum[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ");
                }
            }
        }

        private static bool MatrixAreEquals(int[][] first, int[][] second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if(first[i].Length != second[i].Length)
                {
                    return false;
                }
            }

            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0; j < first[i].Length; j++)
                {
                    if (first[i][j] != second[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
