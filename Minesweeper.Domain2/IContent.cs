using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public interface IContent
    {
        void Uncover();

        bool IsBomb { get; }

        bool IsBlank { get; }

        bool IsNumber { get; }
    }
}
