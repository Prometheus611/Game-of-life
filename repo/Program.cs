using System.Text;

namespace Conwey
{
    internal class Program
    {
        static string Color(int c)
        {
            return c switch
            {
                0 => "\x1b[31m",
                1 => "\x1b[30m",
                _ => "\x1b[92m"
            };
        }
        static bool WindowChanged()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            if (width != previousWidth || height != previousHeight)
            {
                previousWidth = width;
                previousHeight = height;

                Console.Write("\x1b[2J\x1b[H");
                return true;
            }

            return false;
        }
        static int previousWidth = Console.WindowWidth;
        static int previousHeight = Console.WindowHeight;
        static void Main(string[] args)
        {
            if (!AnsiSupport.Enable())
            {
                Console.WriteLine("ANSI support could not be enabled.");
                return;
            }
            int x = 50;
            int y = 50;
            Game Conway = new(x, y);
            Conway.Init();
            int w = Conway.Field.GetLength(0);
            int h = Conway.Field.GetLength(1);
            StringBuilder output = new();
            while (true)
            {
                WindowChanged();
                int col = Math.Max(1, (Console.WindowWidth - w * 2) / 2 + 1);
                int row = Math.Max(1, (Console.WindowHeight - h) / 2 + 1);
                for (int i = 0; i < w; i++)
                {
                    output.Append($"\x1b[{row+i};{col}H");
                    for (int j = 0; j < h; j++)
                    {
                        if (Conway.Field[i, j].Status != 0)
                        {
                            Conway.Field[i, j].GetNeighbours(Conway.Field);
                        }
                        output.Append($"{Color(Conway.Field[i, j].Status)}{Conway.Field[i, j].Status.ToString()} ");
                    }
                    output.AppendLine();
                }
                Console.Write(output.ToString());
                output.Clear();
                for (int i = 1; i < w-1; i++)
                {
                    for (int j = 1; j < h-1; j++)
                    {
                        Conway.Field[i,j].GetNeighbours(Conway.Field);
                    }
                }
                Conway.Step();
                Thread.Sleep(500);
            }
        }
    }
}
