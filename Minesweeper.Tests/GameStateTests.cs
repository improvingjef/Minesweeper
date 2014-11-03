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
    public class GameStateTests
    {
        private MinesweeperGame game;

        [SetUp]
        public void Setup()
        {
            game = new MinesweeperGame(3, 3, 3);
            game.State.Uncover(1, 1);
        }

        [Test]
        public void Should_Transition_To_Initialized_After_The_First_Uncover()
        {
            Assert.IsInstanceOf<InitializedGameState>(game.State);
        }

        [Test]
        public void Should_Initialize_The_Squares()
        {
            Assert.IsTrue(game.Squares.All(s => s.Content != null));
        }
    }
}
