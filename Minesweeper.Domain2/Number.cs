using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class Number : IContent
    {
        public int Count { get; set; }

        public void Uncover()
        {
        }

        public bool IsBomb
        {
            get { return false; }
        }

        public void Increment()
        {
            Count++;
        }


        public bool IsBlank
        {
            get { return false; }
        }

        public bool IsNumber
        {
            get { return true; }
        }
    }
}
