using Minesweeper.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class MinesweeperConsoleViewTests
    {
        [Test]
        public void It_Should_Render_The_Grid()
        {
            var expectedOutput =
@"     1  
 1 |   |
";
            var game = new MinesweeperGame(1, 1, 1);
            var writer = new StringWriter();
            var view = new MinesweeperConsoleView(game, writer, new StringReader(""));
            view.Render();
            var output = writer.ToString();
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
