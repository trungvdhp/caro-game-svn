using System;
using System.Collections.Generic;
using System.Text;

namespace Caro
{
    class Cell
    {
        public char state;
        public Cell(char state)
        {
            this.state = state;
        }
    }
    class CaroBoard
    {
        public Cell[,] cells;
        public bool XPlaying;
        public int size { get; private set; }
        private int[] dx = { 0, 1, -1, 1 };
        private int[] dy = { 1, 0, 1, 1 };
        public bool IsGameOver
        {
            get
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (cells[i, j].state == 'x' || cells[i, j].state == 'o')
                            if (Check5Row(i, j, 1, 4) >= 5)
                                return true;
                return false;
            }
        }
        public int Check5Row(int x,int y,int count,int type)
        {
            if (type == 4)
            {
                int max=0;
                for(int i=0;i<4;i++)
                {
                    int u=x+dx[i];
                    int v=y+dy[i];
                    if (u >= 0 && v >= 0 & u < size && v < size && cells[u, v].state == cells[x, y].state)
                        max = Math.Max(max, Check5Row(u, v, count + 1, i));
                }
                return max;
            }
            else
            {
                int u = x + dx[type];
                int v = y + dy[type];
                if (u >= 0 && v >= 0 & u < size && v < size && cells[u, v].state == cells[x, y].state)
                    return Check5Row(u, v, count + 1, type);
                return count;
            }
        }
        private void New(int n)
        {
            XPlaying = true;
            size = n;
            cells = new Cell[size, size];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    cells[i, j] = new Cell(' ');
        }
        public CaroBoard(int n)
        {
            New(n);
        }
        public CaroBoard():this(14)
        {
        }
    }
}
