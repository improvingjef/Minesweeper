using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class InitializedGameState : IGameState
    {
        public InitializedGameState(MinesweeperGame game)
        {
            Game = game;
        }

        public void Uncover(int x, int y)
        {
            Game.UncoverSquareAt(x, y);
        }

        public MinesweeperGame Game { get; set; }
    }
}
