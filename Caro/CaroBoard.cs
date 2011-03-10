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
        public Position()
        {
        }
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
        public char CurrentPlayer
        {
            get
            {
                return XPlaying ? 'x' : 'o';
            }
        }
        public int size { get; private set; }
        private int[] dx = { 0, 1, -1, 1, 0, -1, 1, -1 };
        private int[] dy = { 1, 0, 1, 1, -1, 0, -1, -1 };
        public Position PrevMove = new Position(-1,-1);
        public Position CurrMove = new Position(-1,-1);
        
        public bool IsGameOver
        {
            get
            {
                char CurrPlayer = XPlaying?'x':'o';
                int count=1;
                bool next=true,prev=true;
                Console.WriteLine("Move {0}-{1}", CurrMove.x, CurrMove.y);
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
        public bool IsGame0ver
        {
            get
            {
                char CurrPlayer = XPlaying ? 'x' : 'o';
                for (int i = 0; i < 4; i++)
                    if (Check5Row(CurrMove.x, CurrMove.y, i, CurrPlayer) >= 5) return true;
                return false;
            }
        }
        public bool CheckPosition(int x,int y)
        {
            return (x >= 0 && y >= 0 && x < size && y < size);
        }
        /// <summary>
        /// Hàm đếm số quân liên kết với 1 quân
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="count"></param>
        /// <param name="type">Kiểu đi</param>
        /// <returns>Số quân cờ liên kết</returns>
        public int Check5Row(int x,int y,int type,char CurrPlayer)
        {
            bool next = true, prev = true;
            int count = 1;
            int u, v;
            for(int i=1;i<5;i++)
            {
                u = x + i * dx[type];
                v = y + i * dy[type];
                if (CheckPosition(u, v) && cells[u, v] == CurrPlayer && next)
                    count++;
                else next = false;
                u = x + i * dx[type + 4];
                v = y + i * dy[type + 4];
                if (CheckPosition(u, v) && cells[u, v] == CurrPlayer && prev)
                    count++;
                else prev = false;
            }
            return count;
        }
        //public void Set(CaroBoard b)
        //{
        //    for (int i = 0; i < size; i++)
        //        for (int j = 0; j < size; j++)
        //            cells[i, j] = b.cells[i, j];
        //}
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
