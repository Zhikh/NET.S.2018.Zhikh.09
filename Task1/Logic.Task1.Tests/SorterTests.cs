using System;
using NUnit.Framework;
using Logic.Task1.Tests.Compares;

namespace Logic.Task1.Tests
{
    [TestFixture]
    public class SorterTests
    {
        #region Test data
        private int[][][] _maxSourceData = new int[][][]
        {
            new int[][]
            {
                null,
                null,
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
                new int[] { 3, 2, 6, 4 },
                new int[] { 5, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 2, 1},
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, int.MaxValue, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4},
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, -390, 3, -400, 11, 8, 9, 0, -777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4},
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                null,
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                null,
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
            }
        };

        private int[][][] _minSourceData = new int[][][]
        {
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                null,
                null
            },
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                new int[] { 6, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            },
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                new int[] { 5, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 2, 1},
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, int.MaxValue, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4},
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, -390, 3, -400, 11, 8, 9, 0, -777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4},
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                null,
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                null,
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
            }
        };

        private int[][][] _sumSourceData = new int[][][]
        {
            new int[][]
            {
                new int[] { 6, 2, 1 },
                new int[] { 3, 2, 6, 4 },
                null
            },
            new int[][]
            {
                new int[] { 3, 2, 6, 4 },
                new int[] { 6, 2, 1 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 2, 1},
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 }
            },
            new int[][]
            {
                new int[] { -1, int.MaxValue, int.MinValue },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 0 },
                new int[] { int.MaxValue },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { -1, int.MaxValue, int.MinValue },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                null,
                null,
                new int[] { int.MaxValue },
                null
            }
        };

        private int[][][] _resultMax = new int[][][]
        {
            new int[][]
            {
                null,
                null,
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
                new int[] { 5, 2, 1 },
                new int[] { 3, 2, 6, 4 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            },
            new int[][]
            {
                new int[] { 2, 1},
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8}
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 }
            },
            new int[][]
            {
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 3, 2, 1, 2, int.MaxValue, 4, 6, 4 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 89, -390, 3, -400, 11, 8, 9, 0, -777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
            },
            new int[][]
            {
                null,
                null,
                new int[] { 3, 2, 1, 2, 4, 6, 4},
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 }
            }
        };

        private int[][][] _resultMin= new int[][][]
        {
            new int[][]
            {
                null,
                null,
                new int[] { 3, 2, 6, 4 },
            },
            new int[][]
            {
                new int[] { 9, 0, 3, 6, 1, 8, 9},
                new int[] { 6, 2, 1 },
                new int[] { 3, 2, 6, 4 },
            },
            new int[][]
            {
                new int[] { 9, 0, 3, 6, 1, 8, 9},
                new int[] { 5, 2, 1 },
                new int[] { 3, 2, 6, 4 }
            },
            new int[][]
            {
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 },
                new int[] { 2, 1}
            },
            new int[][]
            {
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 3, 2, 1, 2, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 89, 390, 3, 400, 11, 8, 9, 0, 777 },
                new int[] { 3, 2, 1, 2, int.MaxValue, 4, 6, 4 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
            },
            new int[][]
            {
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 89, -390, 3, -400, 11, 8, 9, 0, -777 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 3, 2, 1, 2, 4, 6, 4 }
            },
            new int[][]
            {
                null,
                null,
                new int[] { 7, 34, 56, 1, -1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, -576 },
                new int[] { -190, 0, 3, 6, 11, 8, 9, 17 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, -120 },
                new int[] { 3, 2, 1, 2, 4, 6, 4}
            }
        };

        private int[][][] _resultSum = new int[][][]
        {
            new int[][]
            {
                null,
                new int[] { 6, 2, 1 },
                new int[] { 3, 2, 6, 4 }
            },
            new int[][]
            {
                new int[] { 6, 2, 1 },
                new int[] { 3, 2, 6, 4 },
                new int[] { 9, 0, 3, 6, 1, 8, 9}
            },
            new int[][]
            {
                new int[] { 2, 1},
                new int[] { 3, 2, 1, 2, 4, 0, 4 },
                new int[] { 19, 0, 3, 6, 11, 8, 9 },
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8 }
            },
            new int[][]
            {
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { -1, int.MaxValue, int.MinValue },
                new int[] { 0 },
                new int[] { 5, 0, 5, 6, 0, 6, 0 },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { 34, 567, 3, 6, 769, 8, 9, 67, 34, 88, 576 },
                new int[] { int.MaxValue },
            },
            new int[][]
            {
                null,
                null,
                null,
                new int[] { 7, 34, 56, 1, 1, 0, 3, 7, 8, 78, 67, int.MinValue },
                new int[] { -1, int.MaxValue, int.MinValue },
                new int[] { 6, 2, 1, 5, 10, 5, 12, 120 },
                new int[] { int.MaxValue }
            }
        };
        #endregion

        #region Tests
        #region Exceptions
        [Test]
        public void Sort_NullArrayMaxCompare_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => Sorter.Sort(null, new MaxCompare()));

        [Test]
        public void Sort_NullArrayMinCompare_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => Sorter.Sort(null, new MinCompare()));

        [Test]
        public void Sort_NullArraySumCompare_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => Sorter.Sort(null, new SumCompare()));

        [Test]
        public void Sort_ArrayNullCompare_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => Sorter.Sort(_maxSourceData[0], null));
        #endregion

        #region MaxCompare tests
        [Test]
        public void Sort_MaxCompareAsc_SuccessfulTests()
        {
            ICompare<int[]> compare = new MaxCompare();

            for (int i = 0; i < _maxSourceData.Length; i++)
            {
                Sorter.Sort(_maxSourceData[i], compare);

                if (!MatrixAreEquals(_maxSourceData[i], _resultMax[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }

        [Test]
        public void Sort_MaxCompareDec_SuccessfulTests()
        {
            ICompare<int[]> compare = new MaxCompare(false);
            foreach (var subarray in _resultMax)
            {
                Array.Reverse(subarray);
            }

            for (int i = 0; i < _maxSourceData.Length; i++)
            {
                Sorter.Sort(_maxSourceData[i], compare);

                if (!MatrixAreEquals(_maxSourceData[i], _resultMax[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }
        #endregion

        #region MinCompare tests
        [Test]
        public void Sort_MinCompareAsc_SuccessfulTests()
        {
            ICompare<int[]> compare = new MinCompare();

            for (int i = 0; i < _minSourceData.Length; i++)
            {
                Sorter.Sort(_minSourceData[i], compare);

                if (!MatrixAreEquals(_minSourceData[i], _resultMin[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }

        [Test]
        public void Sort_MinCompareDec_SuccessfulTests()
        {
            ICompare<int[]> compare = new MinCompare(false);
            foreach (var subarray in _resultMin)
            {
                Array.Reverse(subarray);
            }

            for (int i = 0; i < _minSourceData.Length; i++)
            {
                Sorter.Sort(_minSourceData[i], compare);

                if (!MatrixAreEquals(_minSourceData[i], _resultMin[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }
        #endregion

        #region SumCompare tests
        [Test]
        public void Sort_SumCompareAsc_SuccessfulTests()
        {
            ICompare<int[]> compare = new SumCompare();

            for (int i = 0; i < _sumSourceData.Length; i++)
            {
                Sorter.Sort(_sumSourceData[i], compare);

                if (!MatrixAreEquals(_sumSourceData[i], _resultSum[i]))
                {
                    Assert.Fail($"Arrays of {0} index don't equal! ", i);
                }
            }
        }

        [Test]
        public void Sort_SumCompareDec_SuccessfulTests()
        {
            ICompare<int[]> compare = new SumCompare(false);
            foreach (var subarray in _resultSum)
            {
                Array.Reverse(subarray);
            }

            for (int i = 0; i < _sumSourceData.Length; i++)
            {
                Sorter.Sort(_sumSourceData[i], compare);

                if (!MatrixAreEquals(_sumSourceData[i], _resultSum[i]))
                {
                    Assert.Fail($"Arrays of {i} index don't equal! ");
                }
            }
        }
        #endregion
        #endregion

        #region Additional methods
        private static bool MatrixAreEquals(int[][] first, int[][] second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                int? firstLength = null,
                    secondLength = null;

                if (first[i] != null)
                {
                    firstLength = first[i].Length;
                }

                if (second[i] != null)
                {
                    secondLength = second[i].Length;
                }

                if (firstLength != secondLength)
                {
                    return false;
                }
            }

            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0; first[i] != null && j < first[i].Length; j++)
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
