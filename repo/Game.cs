using System;
using System.Collections.Generic;
using System.Text;

namespace Conwey
{
    public struct Cell(int x, int y, int status)
    {
        public int Status { get; set; } = status; // 0 = wall, 1 = dead, 2 = alive
        internal readonly int X_pos { get; } = x;
        internal readonly int Y_pos { get; } = y;
        internal int Neighbours { get; set; }
        public void GetNeighbours(Cell[,] Field)
        {
            Neighbours = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (Field[X_pos + i, Y_pos + j].Status == 2) 
                    {
                        Neighbours += 1;
                    }
                }
            }
        }
    }
    public class Game(int w,int h)
    {
        private Random rnd = new();
        public Cell[,] Field = new Cell[w+2, h+2];
        public void Init()
        {
            for (int i = 0; i < w+2; i++)
            {
                for (int j = 0; j < h+2; j++)
                {
                    if (i==0||j==0||i==w+1||j==h+1)
                    {
                        Field[i, j] = new(i,j,0);
                    }
                    else
                    {
                        Field[i, j] = new(i,j,rnd.Next(1,3));
                    }
                }
            }
        }
        public void Step()
        {
            for (int i = 1; i < w+1; i++)
            {
                for (int j = 1; j < h+1; j++)
                {
                    if (Field[i,j].Status == 1)
                    {
                        if (Field[i, j].Neighbours == 3)
                        {
                            Field[i, j].Status = 2;
                        }
                    }
                    else
                    {
                        switch (Field[i, j].Neighbours)
                        {
                            case < 2:
                            case > 3:
                                Field[i, j].Status = 1;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
