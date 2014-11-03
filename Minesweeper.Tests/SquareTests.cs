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
    public class SquareTests
    {
        [Test]
        public void Knows_It_Contents()
        {
            IContent content = new Bomb(null);
            var square = new Square(1, 1);
            square.Content = content;
        }

        [Test]
        public void Knows_Its_X_Coordinate()
        {
            var expected = 1;
            var square = new Square(expected, 1);
            Assert.AreEqual(expected, square.X);
        }

        [Test]
        public void Knows_Its_Y_Coordinate()
        {
            var expected = 1;
            var square = new Square(expected, expected);
            Assert.AreEqual(expected, square.Y);
        }

        [Test]
        public void Uncovering_A_Square_Uncovers_Its_Contents()
        {
            var expected = 1;
            var square = new Square(expected, expected);
            var fake = new FakeContent();
            square.Content = fake;
            square.Uncover();
            Assert.IsTrue(fake.IsUncovered);
        }

        [Test]
        public void Is_Markable()
        {
            var square = new Square(1, 1);
            square.Mark();
            Assert.IsTrue(square.IsMarked);
        }

        [Test]
        public void Is_Unmarkable()
        {
            var square = new Square(1, 1);
            square.Mark();
            square.Unmark();
            Assert.IsFalse(square.IsMarked);
        }
    }

    class FakeContent : IContent
    {

        public void Uncover()
        {
            IsUncovered = true;
        }

        public bool IsUncovered { get; set; }

        public bool IsBomb { get { return false; } }
    }

}
