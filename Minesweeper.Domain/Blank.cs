using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class Blank : IContent
    {
        private List<Square> neighbors;

        public Blank(List<Square> list)
        {
            this.neighbors = list;
        }

        public void Uncover()
        {
            neighbors.Do(s => s.Uncover());
        }

        public bool IsBomb { get { return false; } }
    }
}
