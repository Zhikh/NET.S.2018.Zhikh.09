namespace Logic.Task1
{
    public interface IComparer<T>
    {
        /// <summary>
        /// Compare T elements
        /// </summary>
        /// <param name="left"> T value for comparing </param>
        /// <param name="right"> T value for comparing  </param>
        /// <returns> Result of comparing (left == right - 0, > - -1, else 1 </returns>
        int Compare(T left, T right);
    }
}
