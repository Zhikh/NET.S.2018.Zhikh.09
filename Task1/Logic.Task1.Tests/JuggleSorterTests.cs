using System;
using NUnit.Framework;

namespace Logic.Task1.Tests
{
    [TestFixture]
    public class JuggleSorterTests
    {
        #region Test data
        Enum[] mods = new Enum[]
        {
            #region Sum
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            #endregion

            #region Min
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            #endregion

            #region Max
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            JuggleSort.Mode.Ascending,
            JuggleSort.Mode.Decreasing,
            #endregion
        };

        Enum[] values = new Enum[]
        {
            #region Sum
            JuggleSort.Value.Sum,
            JuggleSort.Value.Sum,
            JuggleSort.Value.Sum,
            JuggleSort.Value.Sum,
            #endregion

            #region Min
            JuggleSort.Value.Min,
            JuggleSort.Value.Min,
            JuggleSort.Value.Min,
            JuggleSort.Value.Min,
            #endregion
            
            #region Max
            JuggleSort.Value.Max,
            JuggleSort.Value.Max,
            JuggleSort.Value.Max,
            JuggleSort.Value.Max
            #endregion
        };

        private int[][][] _sourceData = new int[][][]
        {
            #region Sum
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
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 1, 5 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 0, 3, 6, 11, 8, 9}
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 1, 5 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 0, 3, 6, 11, 8, 9}
            },
            #endregion

            #region Min
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
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 4, 5, 8 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 0, 3, 6, 11, 8, 9}
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 4, 5, 8 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 0, 3, 6, 11, 8, 9}
            },
            #endregion
            
            #region Max
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 4, 5, 8 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 3, 6, 11, 8, 9},
                new int[] { 6, 2, 5, 8 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 4, 5, 8 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8},
                new int[] { 19, 3, 6, 11, 8, 9},
                new int[] { 6, 2, 5, 8 }
            },
            new int[][]
            {
                new int[] {2, 12, 8, 15, 8},
                new int[] {7, 5, 11, 18, 14},
                new int[] {11, 7, 14, 4, 17, 18},
                new int[] {32, 31, int.MaxValue}
            },
            new int[][]
            {
                new int[] {2, 12, 8, 15, 8},
                new int[] {7, 5, 11, 18, 14},
                new int[] {11, 7, 14, 4, 17, 18},
                new int[] {32, 31, int.MaxValue}
            }
            #endregion
        };

        private int[][][] _resultSum = new int[][][]
        {
            #region Sum
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
            },
            new int[][]
            {
                new int[] { 1, 2, 5, 6 },
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
                new int[] { 0, 3, 6, 8, 9, 11, 19},
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56}
            },
            new int[][]
            {
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56},
                new int[] { 0, 3, 6, 8, 9, 11, 19},
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
                new int[] { 1, 2, 5, 6 }
            },
            #endregion

            #region Min
            new int[][]
            {
                new int[] { 0, 1, 3, 6, 8, 9, 9 },
                new int[] { 1, 2, 6 },
                new int[] { 2, 3, 4, 6 }
            },
            new int[][]
            {
                new int[] { 2, 3, 4, 6 },
                new int[] { 1, 2, 6 },
                new int[] { 0, 1, 3, 6, 8, 9, 9 }
            },
            new int[][]
            {
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56},
                new int[] { 0, 3, 6, 8, 9, 11, 19},
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
                new int[] { 2, 4, 5, 6, 8 }
            },
            new int[][]
            {
                new int[] { 2, 4, 5, 6, 8 },
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
                new int[] { 0, 3, 6, 8, 9, 11, 19},
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56},
            },
            #endregion

            #region Max
            new int[][]
            {
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
                new int[] { 2, 4, 5, 6, 8 },
                new int[] { 2, 5, 6, 8 },
                new int[] { 3, 6, 8, 9, 11, 19},
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56}
            },
             new int[][]
            {
                new int[] { 0, 1, 1, 3, 7, 7, 8, 34, 56},
                new int[] { 3, 6, 8, 9, 11, 19},
                new int[] { 2, 5, 6, 8 },
                new int[] { 2, 4, 5, 6, 8 },
                new int[] { 1, 2, 2, 3, 4, 4, 6 },
            },
            new int[][]
            {
                new int[] {2, 8, 8, 12, 15},
                new int[] {5, 7, 11, 14, 18},
                new int[] {4, 7, 11, 14, 17, 18},
                new int[] {31, 32, int.MaxValue}
            },
            new int[][]
            {
                new int[] {31, 32, int.MaxValue},
                new int[] {4, 7, 11, 14, 17, 18},
                new int[] {5, 7, 11, 14, 18},
                new int[] {2, 8, 8, 12, 15},
            }
            #endregion
        };
        #endregion

        #region Tests
        [Test]
        public void Sort_Null_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => JuggleSort.Sort(null));

        [Test]
        public void Sort_JuggleArray_SuccessfulTests()
        {
            for (int i = 0; i < _sourceData.Length; i++)
            {
                JuggleSort.Sort(_sourceData[i], value: (JuggleSort.Value)values[i], mode: (JuggleSort.Mode)mods[i]);

                if (!MatrixAreEquals(_sourceData[i], _resultSum[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }
        #endregion

        #region Additional methods
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
        #endregion
    }
}
