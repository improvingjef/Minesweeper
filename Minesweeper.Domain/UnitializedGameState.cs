using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Minesweeper.Domain;

namespace Minesweeper.Domain
{
    public class UnitializedGameState : IGameState
    {
        public UnitializedGameState(MinesweeperGame game)
        {
            this.Game = game;
        }

        public void Uncover(int x, int y)
        {
            InitializeSquares(x, y);
            Game.State = new InitializedGameState(Game);
            Game.State.Uncover(x, y);
        }

        private void InitializeSquares(int x, int y)
        {
            InitializeBombs(x, y);
            InitializeNumberSquares();
            FillRemainingSquaresWithBlanks();
        }

        private void InitializeNumberSquares()
        {
            Game.BombSquares
                .SelectMany(square => Game.GetNeighborsOf(square).Where(s => s.IsBomb == false))
                .Do(neighbor => SetOrIncrementNeighboringBombCount(neighbor));
        }

        private void SetOrIncrementNeighboringBombCount(Square neighbor)
        {
            if (neighbor.Content == null)
            {
                neighbor.Content = new Number() { Count = 1 };
            }
            else
            {
                var number = neighbor.Content as Number;
                number.Increment();
            }
        }

        private void FillRemainingSquaresWithBlanks()
        {
            Game.EmptySquares.Do(square => square.Content = new Blank(Game.GetNeighborsOf(square)));
        }

        private void InitializeBombs(int x, int y)
        {
            var random = new Random();

            Game.Squares
                .Where(s => !s.At(x, y))
                .OrderBy(s => random.Next(Game.Squares.Count - 1))
                .Take(Game.Bombs)
                .Do(square => square.Content = new Bomb(Game));
        }

        public MinesweeperGame Game { get; set; }
    }
}
