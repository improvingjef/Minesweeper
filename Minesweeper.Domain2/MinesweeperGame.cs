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

        public bool IsOver { get; set; }

        public void End()
        {
            IsOver = true;
        }

        public List<Square> Squares { get; set; }

        public IGameState State { get; set; }

        public List<Square> BombSquares { get { return Squares.Where(s => s.IsBomb).ToList(); } }


        public List<Square> EmptySquares { get { return Squares.Where(s => s.Content == null).ToList(); } }
    }
}
