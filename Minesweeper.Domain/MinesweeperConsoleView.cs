using Minesweeper.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Minesweeper.Domain
{
    public class MinesweeperConsoleView
    {
        private MinesweeperGame game;
        private TextWriter writer;
        private TextReader reader;

        public MinesweeperConsoleView(MinesweeperGame game, TextWriter writer, TextReader reader)
        {
            this.game = game;
            this.writer = writer;
            this.reader = reader;
        }

        public void Render()
        {
            DrawXIndexes();

            for (var y = 1; y <= game.Rows; y++)
            {
                DrawRow(y);
            }
        }

        private void DrawRow(int y)
        {
            writer.Write(" ");
            writer.Write(y);
            writer.Write(" |");

            for (var x = 1; x <= game.Columns; x++)
            {
                DrawSquareAt(y, x);
            }
            writer.WriteLine();
        }

        private void DrawSquareAt(int y, int x)
        {
            var square = game.SquareAt(x, y);

            writer.Write(" ");

            WriteSquare(square);

            writer.Write(" |");
        }

        private void WriteSquare(Square square)
        {
            if (square.IsUncovered)
            {
                DrawContent(square.Content);
            }
            else if (square.IsMarked)
            {
                DrawMarker();
            }
            else
            {
                DrawCover();
            }
        }

        private void DrawCover()
        {
            writer.Write(" ");
        }

        private void DrawMarker()
        {
            WriteWithColor(">", ConsoleColor.Yellow);
        }

        private void DrawContent(IContent content)
        {
            if (content.IsBomb)
            {
                DrawBomb();
            }
            else if (content is Blank)
            {
                DrawBlank();
            }
            else if (content is Number)
            {
                DrawNumber(content as Number);
            }
        }

        private void DrawNumber(Number number)
        {
            WriteWithColor(number.Count.ToString(), ConsoleColor.Cyan);
        }

        private void DrawBlank()
        {
            WriteWithColor("0", ConsoleColor.Green);
        }

        private void DrawBomb()
        {
            WriteWithColor("*", ConsoleColor.Red);
        }

        private void DrawXIndexes()
        {
            writer.Write("    ");
            for (int x = 1; x <= game.Rows; x++)
            {
                writer.Write(" ");
                writer.Write(x);
                writer.Write("  ");
            }
            writer.WriteLine();
        }

        private void WriteWithColor(string s, ConsoleColor color)
        {
            var foreground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writer.Write(s);
            Console.ForegroundColor = foreground;
        }

        public void Show()
        {
            while (game.IsOver == false)
            {
                DrawUI();

                writer.WriteLine(" ");
                writer.WriteLine("Mark (m), Uncover (u), or Quit (q)");

                var input = reader.ReadLine().ToLower();
                switch (input)
                {
                    case "m":
                        Mark(game);
                        break;

                    case "u":
                        Uncover(game);
                        break;

                    case "q":
                        GameOver();
                        return;

                    default:
                        InvalidCommand();
                        break;
                }
            }
            DrawUI();

            GameOver();
        }

        private void DrawUI()
        {
            Console.Clear();
            Render();
        }

        private void InvalidCommand()
        {
            writer.WriteLine("Invalid command, please try again.");
        }

        private void GameOver()
        {
            writer.WriteLine("Game Over!");
        }

        private void Uncover(MinesweeperGame game)
        {
            writer.WriteLine("X?");
            var ux = reader.ReadLine();
            writer.WriteLine("Y?");
            var uy = reader.ReadLine();
            game.Uncover(Convert.ToInt32(ux), Convert.ToInt32(uy));
        }

        private void Mark(MinesweeperGame game)
        {
            writer.WriteLine("X?");
            var mx = reader.ReadLine();
            writer.WriteLine("Y?");
            var my = reader.ReadLine();
            game.Mark(Convert.ToInt32(mx), Convert.ToInt32(my));
        }

    }
}
