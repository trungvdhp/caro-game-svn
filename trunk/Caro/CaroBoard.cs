using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    class Position
    {
        public int x;
        public int y;
        public void Set(Position p)
        {
            x = p.x;y=p.y;
        }
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
        public Position PrevMove = new Position(-1,-1);
        public Position CurrMove = new Position(-1,-1);
        /// <summary>
        /// THuộc tính kiểm tra GameOver theo phương pháp kiểm tra 5 quân liên tiếp với từng quân
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                //for (int i = 0; i < size; i++)
                //    for (int j = 0; j < size; j++)
                //        if (cells[i, j] == 'x' || cells[i, j] == 'o')
                //            if (Check5Row(i, j, 1, 4) >= 5)
                //                return true;
                //return false;
                char CurrPlayer = XPlaying?'x':'o';
                int count=1;
                bool next=true,prev=true;
                //Console.WriteLine("{0}-{1}:\n", CurrMove.x, CurrMove.y);
                for (int i = 1; i < 5; i++)
                {
                    if (CurrMove.x + i < size && cells[CurrMove.x + i, CurrMove.y] == CurrPlayer && next)
                        count++;
                    else next = false;
                    if (CurrMove.x - i >=0 && cells[CurrMove.x - i, CurrMove.y] == CurrPlayer && prev)
                        count++;
                    else prev = false;
                }
                if (count >= 5) return true;
                count = 1;
                next = prev = true;
                for (int i = 1; i < 5; i++)
                {
                    if (CurrMove.y + i < size && cells[CurrMove.x,CurrMove.y+i] == CurrPlayer && next)
                        count++;
                    else next = false;
                    if (CurrMove.y - i >= 0 && cells[CurrMove.x, CurrMove.y - i] == CurrPlayer && prev)
                        count++;
                    else prev = false;
                }
                if (count >= 5) return true;
                count = 1;
                next = prev = true;
                for (int i = 1; i < 5; i++)
                {
                    if (CurrMove.x + i < size && CurrMove.y+i<size && cells[CurrMove.x + i, CurrMove.y+i] == CurrPlayer && next)
                        count++;
                    else next = false;
                    if (CurrMove.x - i >=0 && CurrMove.y - i >=0 && cells[CurrMove.x - i, CurrMove.y-i] == CurrPlayer && prev)
                        count++;
                    else prev = false;
                }
                if (count >= 5) return true;
                count = 1;
                next = prev = true;
                //Console.WriteLine("{0}-{1}:\n", CurrMove.x, CurrMove.y);
                for (int i = 1; i < 5; i++)
                {
                    if (CurrMove.x + i < size && CurrMove.y - i >= 0 && cells[CurrMove.x + i, CurrMove.y - i] == CurrPlayer && next)
                    {
                        count++;
                        //Console.WriteLine("next {0}-{1}-{2}\n", CurrMove.x + i, CurrMove.y - i,count);
                    }
                    else next = false;
                    if (CurrMove.x - i >= 0 && CurrMove.y + i < size && cells[CurrMove.x - i, CurrMove.y + i] == CurrPlayer && prev)
                    {
                        count++;
                        //Console.WriteLine("prev {0}-{1}-{2}-{3}-{4}-{5}\n", CurrMove.x - i, CurrMove.y + i, count, cells[CurrMove.x - i, CurrMove.y + i], CurrPlayer,prev);
                        
                    }
                    else prev = false;
                }
                if (count >= 5) return true;
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
