using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task1
{
    public interface ICompare<T>
    {
        int Compare(T left, T right);
    }
}
