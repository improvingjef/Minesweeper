using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public interface ISquareState
    {

        void Uncover();

        void Mark();

        void Unmark();
    }

    public abstract class SquareState : ISquareState
    {
        public SquareState(Square square)
        {
            this.square = square;
        }

        protected Square square;


        public virtual void Mark()
        {
        }

        public virtual void Unmark()
        {
        }

        public virtual void Uncover()
        {
        }
    }

    public class CoveredState : SquareState
    {
        public CoveredState(Square square):base(square)
        {
        }

        public override void Uncover()
        {
            square.State = new UncoveredState(square);
            square.Content.Uncover();
        }

        public override void Mark()
        {
            square.IsMarked = true;
            square.State = new MarkedState(square);
        }
    }

    public class UncoveredState : SquareState
    {
        public UncoveredState(Square square):base(square)
        {
        }
    }

    public class MarkedState : CoveredState
    {
        public MarkedState(Square square):base(square)
        {
        }

        public override void Unmark()
        {
            square.IsMarked = false;
            square.State = new CoveredState(square);
        }
    }
}
