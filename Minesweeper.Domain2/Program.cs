using Minesweeper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        private static System.IO.TextWriter writer;
        private static bool IsOver;
        private static int Rows;
        private static int Columns;
        private static List<Square> Squares;
        private static char[,] squares;

        // determine where to set "IsOver" if necessary

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Minesweeper <rows> <columns> <bombs>");
                return;
            }

            Rows = Convert.ToInt32(args[0]);
            Columns = Convert.ToInt32(args[1]);
            var bombs = Convert.ToInt32(args[2]);
            squares = new char[Rows, Columns];

            Squares = new List<Square>();

            for (int x = 1; x <= Columns; x++)
            {
                for (int y = 1; y <= Rows; y++)
                {
                    Squares.Add(new Square(x, y));
                }
            }

            bool allocated = false;

            var random = new Random();

            writer = Console.Out;
            var reader = Console.In;

            while (IsOver == false)
            {
                Console.Clear();

                writer.Write("    ");
                for (int x = 1; x <= Rows; x++)
                {
                    writer.Write(" ");
                    writer.Write(x);
                    writer.Write("  ");
                }
                writer.WriteLine();

                for (var y = 1; y <= Rows; y++)
                {
                    var count = Rows.ToString().Length;
                    writer.Write(y.ToString().PadLeft(count + 1, ' '));
                    writer.Write(" |");

                    for (var x = 1; x <= Columns; x++)
                    {
                        var square = SquareAt(x, y);

                        writer.Write(" ");

                        if (square.IsUncovered)
                        {
                            if (square.Content.IsBomb)
                            {
                                WriteWithColor("*", ConsoleColor.Red);
                            }
                            else if (square.Content is Blank)
                            {
                                WriteWithColor("0", ConsoleColor.Green);
                            }
                            else if (square.Content is Number)
                            {
                                WriteWithColor((square.Content as Number).Count.ToString(), ConsoleColor.Cyan);
                            }
                        }
                        else if (square.IsMarked)
                        {
                            WriteWithColor(">", ConsoleColor.Yellow);
                        }
                        else
                        {
                            writer.Write(" ");
                        }

                        writer.Write(" |");
                    }
                    writer.WriteLine();
                }

                Console.Out.WriteLine(" ");

                Console.Out.WriteLine("Mark (m), Uncover (u), or Quit (q)");

                var input = Console.In.ReadLine().ToLower();

                switch (input)
                {
                    case "m":
                        writer.WriteLine("X?");
                        var mx = reader.ReadLine();
                        writer.WriteLine("Y?");
                        var my = reader.ReadLine();

                        SquareAt(Convert.ToInt32(mx), Convert.ToInt32(my)).Mark();
                        break;

                    case "u":
                        writer.WriteLine("X?");
                        var ux = reader.ReadLine();
                        writer.WriteLine("Y?");
                        var uy = reader.ReadLine();

                        if (!allocated)
                        {

                            Squares.Where(s => !s.At(Convert.ToInt32(ux), Convert.ToInt32(uy)))
                            .OrderBy(s => random.Next(Squares.Count - 1))
                            .Take(bombs)
                            .Do(square => square.Content = new Bomb(null));

                            Squares.Where(s => s.IsBomb)
                            .SelectMany(square => GetNeighborsOf(square).Where(s => s.IsBomb == false))
                            .Do(neighbor =>
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
                            });

                            Squares.Where(c => c.Content == null).Do(square => square.Content = new Blank(GetNeighborsOf(square)));
                        }

                        var s = SquareAt(Convert.ToInt32(ux), Convert.ToInt32(uy));

                        s.IsUncovered = true;

                        if( s.IsBomb) 
                        {
                            IsOver = true;
                        }
                        else if(s.Content.IsBlank)
                        {
                            var neighbors = GetNeighborsOf(s);
                            foreach(var neighbor in neighbors)
                            {
                                neighbor.IsUncovered = true;
                            }
                        }
                            
                        break;

                    case "q":
                        writer.WriteLine("Game Over!");
                        return;

                    default:
                        writer.WriteLine("Invalid command, please try again.");
                        break;
                }
            }

            Console.Clear();
            writer.Write("    ");
            for (int x = 1; x <= Rows; x++)
            {
                writer.Write(" ");
                writer.Write(x);
                writer.Write("  ");
            }
            writer.WriteLine();

            for (var y = 1; y <= Rows; y++)
            {
                var count = Rows.ToString().Length;

                writer.Write(y.ToString().PadLeft(count + 1, ' '));

                writer.Write(" |");

                for (var x = 1; x <= Columns; x++)
                {

                    var square = SquareAt(x, y);

                    writer.Write(" ");

                    if (square.IsUncovered)
                    {
                        if (square.Content.IsBomb)
                        {
                            WriteWithColor("*", ConsoleColor.Red);
                        }
                        else if (square.Content is Blank)
                        {
                            WriteWithColor("0", ConsoleColor.Green);
                        }
                        else if (square.Content is Number)
                        {
                            WriteWithColor((square.Content as Number).Count.ToString(), ConsoleColor.Cyan);
                        }
                    }
                    else if (square.IsMarked)
                    {
                        WriteWithColor(">", ConsoleColor.Yellow);
                    }
                    else
                    {
                        writer.Write(" ");
                    }

                    writer.Write(" |");
                }
                writer.WriteLine();
            }

            writer.WriteLine("Game Over!");

            Console.ReadLine();
        }

        public static Square SquareAt(int x, int y)
        {
            return Squares.FirstOrDefault(s => s.X == x && s.Y == y);
        }

        public static void WriteWithColor(string s, ConsoleColor color)
        {
            var foreground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writer.Write(s);
            Console.ForegroundColor = foreground;
        }

        public static List<Square> GetNeighborsOf(Square square)
        {
            var minX = square.X == 1 ? 1 : square.X - 1;
            var minY = square.Y == 1 ? 1 : square.Y - 1;
            var maxX = square.X == Columns ? Columns : square.X + 1;
            var maxY = square.Y == Rows ? Columns : square.Y + 1;

            return Squares.Where(
                s => s != square
                    && s.X >= minX && s.X <= maxX
                    && s.Y >= minY && s.Y <= maxY
                ).ToList();
        }
    }
}
