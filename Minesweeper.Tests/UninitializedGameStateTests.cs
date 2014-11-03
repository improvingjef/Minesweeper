using Minesweeper.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class UninitializedGameStateTests
    {
        [Test]
        public void Should_Allocate_Bombs_When_Uncovering()
        {
            var game = new MinesweeperGame(5, 5, 5);
            game.State.Uncover(1, 1);
            Assert.AreEqual(5, game.Squares.Count(s => s.Content.IsBomb));
        }

        [Test]
        public void Should_Not_Place_A_Bomb_In_The_First_Uncovered_Square()
        {
            for(int i = 0; i < 100; i++)
            {
                var game = new MinesweeperGame(5, 5, 5);
                game.State.Uncover(1, 1);
                Assert.IsFalse(game.SquareAt(1,1).Content.IsBomb);
            }
        }
    }
}
