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
    public class BombTests
    {
        [Test]
        public void Uncovering_A_Bomb_Ends_The_Game()
        {
            var game = new MinesweeperGame(1, 1, 1);
            var bomb = new Bomb(game);
            bomb.Uncover();
            Assert.IsTrue(game.IsOver);
        }
    }
}
