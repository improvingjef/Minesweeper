using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class Square
    {
        public Square(int x, int y)
        {
            X = x;
            Y = y;

            State = new CoveredState(this);
        }

        public IContent Content { get; set; }

        public int X { get; set; }
        
        public int Y { get; set; }
        
        public bool IsMarked { get; set; }

        public void Uncover()
        {
            State.Uncover();
        }

        public void Mark()
        {
            State.Mark();
        }

        public void Unmark()
        {
            State.Unmark();
        }

        public bool At(int x, int y)
        {
            return X == x && Y == y;
        }

        public bool IsBomb { get { return Content != null && Content.IsBomb; } }

        public bool IsUncovered { get; set; }

        public ISquareState State { get; set; }

        public override string ToString()
        {
            return "["+Content == null ? "Uninitialized" : Content.GetType().Name +"]";
        }
    }
}
