using System;
using System.Collections.Generic;
using System.Text;

namespace Caro
{
    class Position
    {
        public int x;
        public int y;
        public void Set(int a, int b)
        {
            x = a; y = b;
        }
        public Position(int a, int b)
        {
            x = a; y = b;
        }
    }
    class CaroBoard
    {
        public char[,] cells;
        public bool XPlaying;
        public int size { get; private set; }
        private int[] dx = { 0, 1, -1, 1 };
        private int[] dy = { 1, 0, 1, 1 };
        /// <summary>
        /// THuộc tính kiểm tra GameOver theo phương pháp kiểm tra 5 quân liên tiếp với từng quân
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (cells[i, j] == 'x' || cells[i, j] == 'o')
                            if (Check5Row(i, j, 1, 4) >= 5)
                                return true;
                return false;
            }
        }
        /// <summary>
        /// Hàm đếm số quân liên kết với 1 quân
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="count"></param>
        /// <param name="type">Kiểu đi</param>
        /// <returns>Số quân cờ liên kết</returns>
        public int Check5Row(int x,int y,int count,int type)
        {
            if (type == 4)
            {
                int max=0;
                for(int i=0;i<4;i++)
                {
                    int u=x+dx[i];
                    int v=y+dy[i];
                    if (u >= 0 && v >= 0 & u < size && v < size && cells[u, v] == cells[x, y])
                        max = Math.Max(max, Check5Row(u, v, count + 1, i));
                }
                return max;
            }
            else
            {
                int u = x + dx[type];
                int v = y + dy[type];
                if (u >= 0 && v >= 0 & u < size && v < size && cells[u, v] == cells[x, y])
                    return Check5Row(u, v, count + 1, type);
                return count;
            }
        }
        private void New(int n)
        {
            XPlaying = true;
            size = n;
            cells = new char[size, size];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    cells[i, j] =' ';
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
