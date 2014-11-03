using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class MinesweeperGame
    {
        public MinesweeperGame(int rows, int columns, int bombs = 0)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Bombs = bombs;
            this.State = new UnitializedGameState(this);

            Squares = new List<Square>();

            for (int x = 1; x <= columns; x++)
            {
                for (int y = 1; y <= rows; y++)
                {
                    Squares.Add(new Square(x, y));
                }
            }
        }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Bombs { get; set; }

        public void Uncover(int x, int y)
        {
            State.Uncover(x, y);
        }

        public Square SquareAt(int x, int y)
        {
            return Squares.FirstOrDefault(s => s.X == x && s.Y == y);
        }

        public void Mark(int x, int y)
        {
            SquareAt(x, y).Mark();
        }

        public void Unmark(int x, int y)
        {
            SquareAt(x, y).Unmark();
        }

        public bool IsOver { get; set; }

        public void End()
        {
            IsOver = true;
        }

        public List<Square> Squares { get; set; }

        public IGameState State { get; set; }

        public void UncoverSquareAt(int x, int y)
        {
            SquareAt(x, y).Uncover();
        }

        public List<Square> BombSquares { get { return Squares.Where(s => s.IsBomb).ToList(); } }

        public List<Square> GetNeighborsOf(Square square)
        {
            var minX = square.X == 1 ? 1 : square.X - 1;
            var minY = square.Y == 1 ? 1 : square.Y - 1;
            var maxX = square.X == Columns ? Columns : square.X + 1;
            var maxY = square.Y == Rows ? Columns : square.Y + 1;

            return Squares.Where(
                s => s != square
                    && s.X >= minX && s.X <= maxX
                    && s.Y >= minY && s.Y <= maxY
                ).ToList();
        }

        public List<Square> EmptySquares { get { return Squares.Where(s => s.Content == null).ToList(); } }
    }
}
