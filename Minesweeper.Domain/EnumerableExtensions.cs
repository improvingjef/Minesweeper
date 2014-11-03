using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Do<T>(this IEnumerable<T> ts, Action<T> toDo)
        {
            foreach (var t in ts) toDo(t);

            return ts;
        }
    }
}
