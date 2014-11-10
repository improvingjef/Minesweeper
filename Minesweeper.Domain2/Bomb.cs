using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class Bomb : IContent
    {
        private MinesweeperGame game;

        public Bomb(MinesweeperGame game)
        {
            this.game = game;
        }

        public void Uncover()
        {
            game.End();
        }


        public bool IsBomb { get { return true; } }


        public bool IsBlank
        {
            get { return false; }
        }

        public bool IsNumber
        {
            get { return false; }
        }
    }
}
