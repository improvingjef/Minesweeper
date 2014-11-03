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
    public class MinesweeperGameTests
    {
        [Test]
        public void Knows_Its_Rows()
        {
            int rows = 1;
            var game = new MinesweeperGame(rows, 0);
            Assert.AreEqual(rows, game.Rows);
        }

        [Test]
        public void Knows_Its_Columns()
        {
            int columns = 1;
            int rows = 1;

            var game = new MinesweeperGame(rows, columns);
            Assert.AreEqual(columns, game.Columns);
        }

        [Test]
        public void When_A_Bomb_Is_Uncovered_The_Game_Will_End()
        {
            int rows = 1;
            int columns = 1;
            int bombs = 1;

            var game = new MinesweeperGame(rows, columns, bombs);
            game.Squares[0].Content = new Bomb(game);
            game.Uncover(1, 1);
            Assert.IsTrue(game.IsOver);
        }

        [Test]
        public void A_Game_Is_Not_Ended_Before_It_Starts()
        {
            int rows = 1;
            int columns = 1;
            int bombs = 1;

            var game = new MinesweeperGame(rows, columns, bombs);

            Assert.IsFalse(game.IsOver);
        }

        [Test]
        public void Should_Have_Row_Times_Column_Squares()
        {
            int rows = 1;
            int columns = 1;
            int bombs = 1;

            var game = new MinesweeperGame(rows, columns, bombs);
            Assert.AreEqual(1, game.Squares.Count);

            game = new MinesweeperGame(2, 2, bombs);
            Assert.AreEqual(4, game.Squares.Count);
        }

        [Test]
        public void All_Squares_Should_Lack_Content_Before_The_First_Uncover()
        {
            int rows = 5;
            int columns = 5;
            int bombs = 1;

            var game = new MinesweeperGame(rows, columns, bombs);
            Assert.IsTrue(game.Squares.All(s=>s.Content == null));
        }

        [Test]
        public void All_Squares_Should_Have_Content_After_The_First_Uncover()
        {
            int rows = 5;
            int columns = 5;
            int bombs = 1;

            var game = new MinesweeperGame(rows, columns, bombs);
            game.Uncover(1, 1);
            Assert.IsTrue(game.Squares.All(s => s.Content != null));
        }

        [Test]
        public void The_Game_Should_Be_Uninitialized_By_Default()
        {
            var game = new MinesweeperGame(1, 1);
            Assert.IsInstanceOf<UnitializedGameState>(game.State);
        }

        [Test]
        public void Should_Unmark_The_Square_At_The_Specified_Coordinates()
        {
            var game = new MinesweeperGame(1, 1, 1);
            game.Unmark(1, 1);
            Assert.IsFalse(game.SquareAt(1, 1).IsMarked);
        }

        [Test]
        public void Should_Mark_The_Square_At_The_Specified_Coordinates()
        {
            var game = new MinesweeperGame(1, 1, 1);
            game.Mark(1, 1);
            Assert.IsTrue(game.SquareAt(1, 1).IsMarked);
        }

        // randomly allocate bombs
        // count neighboring bombs
        // render output
    }
}
