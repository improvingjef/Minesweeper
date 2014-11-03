using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public interface IGameState
    {
        void Uncover(int x, int y);
    }
}
