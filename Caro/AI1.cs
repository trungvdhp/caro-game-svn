using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace Caro
{
    class State
    {
        public Position p;
        public int val;
        public State(Position p,int val)
        {
            this.p=p;
            this.val=val;
        }
        public void Set(Position p, int val)
        {
            this.p = p;
            this.val = val;
        }
    }
    class AI1
    {
        int n;
        int[] TScore = { 0, 1, 9, 85, 769 };
        int[] KScore = { 0, 2, 28, 256, 2308 };
        int[,] Val;
        private int maxdepth;
        char _computer;
        char _player;
        int _branch;
        const int INT_MAX = 2147483647;
        public char computer
        {
            get{
                return _computer;
            }
            set{
                if (value == 'x')
                    _player = 'o';
                else
                    _player = 'x';
                _computer=value;
            }
        }
        void ResetVal()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Val[i, j] = 0;
        }
        public AI1(int size, char computer)
        {
            n = size;
            this.computer = computer;
            Val = new int[n, n];
            _branch = 8;
            maxdepth = 3;
        }
        public void EvalueCaroBoard(ref CaroBoard b,char Player)
        {
            n=b.size;
            ResetVal();
            int rw, cl, i;
            int cComputer, cPlayer;
            //kiem tra hang
            for(rw=0;rw<n;rw++)
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw, cl + i] == _computer) cComputer++;
                        if (b.cells[rw, cl + i] == _player) cPlayer++;
                    }
                    if(cComputer*cPlayer==0&&cComputer!=cPlayer)
                        for(i=0;i<5;i++)
                            if(b.cells[rw,cl+i]==' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw, cl + i] += TScore[cPlayer];
                                    else Val[rw, cl + i] -= KScore[cPlayer];
//                                     if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _computer && b.cells[rw, cl + 5] == _computer)
//                                         Val[rw, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw, cl + i] -= TScore[cComputer];
                                    else Val[rw, cl + i] += KScore[cComputer];
//                                     if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _player && b.cells[rw, cl + 5] == _player)
//                                         Val[rw, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw, cl + i - 1) && b.cells[rw, cl + i - 1] == ' ') || (b.CheckPosition(rw, cl + i + 1) && b.cells[rw, cl + i + 1] == ' ')))
                                    Val[rw, cl + i] *= 2;
                            }
                }
            //Cot
            for (rw = 0; rw < n-4; rw++)
                for (cl = 0; cl < n; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw+i, cl] == _computer) cComputer++;
                        if (b.cells[rw+i, cl] == _player) cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw+i, cl] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw+i, cl] += TScore[cPlayer];
                                    else Val[rw+i, cl] -= KScore[cPlayer];
//                                     if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _computer && b.cells[rw + 5, cl] == _computer)
//                                         Val[rw + i, cl] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw+i, cl] -= TScore[cComputer];
                                    else Val[rw+i, cl] += KScore[cComputer];
//                                     if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _player && b.cells[rw + 5, cl] == _player)
//                                         Val[rw + i, cl] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw+i-1,cl)&&b.cells[rw+i-1, cl] == ' ') || (b.CheckPosition(rw+i+1, cl) && b.cells[rw+i+1, cl] == ' ')))
                                    Val[rw+i, cl] *= 2;
                            }
                }
            //Duong cheo xuong
            for (rw = 0; rw < n - 4; rw++)
                for (cl = 0; cl < n-4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw + i, cl+i] == _computer) cComputer++;
                        if (b.cells[rw + i, cl+i] == _player) cPlayer++;
                    }
                    //Luong gia..
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw + i, cl+i] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw + i, cl+i] += TScore[cPlayer];
                                    else Val[rw + i, cl+i] -= KScore[cPlayer];
//                                     if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _computer && b.cells[rw + 5, cl + 5] == _computer)
//                                         Val[rw + i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw + i, cl+i] -= TScore[cComputer];
                                    else Val[rw + i, cl+i] += KScore[cComputer];
//                                     if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _player && b.cells[rw + 5, cl + 5] == _player)
//                                         Val[rw + i, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw + i - 1, cl+i-1) && b.cells[rw + i - 1, cl+i-1] == ' ') || (b.CheckPosition(rw + i + 1, cl+i+1) && b.cells[rw + i + 1, cl+i+1] == ' ')))
                                    Val[rw + i, cl+i] *= 2;
                            }
                }
            //Duong cheo len
            for (rw = 4; rw < n - 4; rw++)
                for (cl = 0; cl < n-4 ; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw - i, cl + i] == _computer) cComputer++;
                        if (b.cells[rw - i, cl + i] == _player) cPlayer++;
                    }
                    //Luong gia..
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw - i, cl + i] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw - i, cl + i] += TScore[cPlayer];
                                    else Val[rw - i, cl + i] -= KScore[cPlayer];
//                                     if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _computer && b.cells[rw - 5, cl + 5] == _computer)
//                                         Val[rw - i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw - i, cl + i] -= TScore[cComputer];
                                    else Val[rw - i, cl + i] += KScore[cComputer];
//                                     if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _player && b.cells[rw - 5, cl + 5] == _player)
//                                         Val[rw + i, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw - i + 1, cl + i - 1) && b.cells[rw - i + 1, cl + i - 1] == ' ') || (b.CheckPosition(rw - i - 1, cl + i + 1) && b.cells[rw - i - 1, cl + i + 1] == ' ')))
                                    Val[rw - i, cl + i] *= 2;
                            }
                }
            //EchoVal();

        }
        void EchoVal()
        {
            Console.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    
                    Console.Write("{0} ",Val[i, j]);
                }
                Console.WriteLine("\n");
            }
        }
        string Space(int x)
        {
            int k = x.ToString().Length;
            string r = "";
            //if (x < 0) k += 1;
            for (int i = 0; i < 4-k; i++)
                r += " ";
            return r;
        }
        private State GetMaxNode()
        {
            Position p=new Position(0,0);
            int t = -INT_MAX;
            for(int i=0;i<n;i++)
                for(int j=0;j<n;j++)
                    if (t < Val[i, j])
                    {
                        t = Val[i, j];
                        p.Set(i, j);
                    }
            State s=new State(p,Val[p.x,p.y]);
            Val[p.x, p.y] = 0;
            return s;
        }
        private State GetMinNode()
        {
            Position p = new Position(0, 0);
            int t = INT_MAX;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (t > Val[i, j])
                    {
                        t = Val[i, j];
                        p.Set(i, j);
                    }
            State s = new State(p, Val[p.x, p.y]);
            Val[p.x, p.y] = 0;
            return s;
        }
        public Position Solve(ref CaroBoard b, char Player )
        {
            EvalueCaroBoard(ref b, Player);
            Position p = new Position(b.size / 2, b.size / 2);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMaxNode());
            int maxp = -INT_MAX;

            for (int i = 0; i < _branch;i++ )
            {
                b.cells[list[i].p.x, list[i].p.y] = _computer;
                int t=MinVal(ref b, list[i], -INT_MAX, INT_MAX,0);
                if (maxp < t) 
                {
                    maxp=t;
                    p.Set(list[i].p);
                }
                b.cells[list[i].p.x, list[i].p.y] = ' ';
            }
            //p.Set(list[0].p);
            return p;
        }
        private int MaxVal(ref CaroBoard b,State s,int alpha, int beta, int depth)
        {
            if (s.val >= KScore[4] || depth>=maxdepth) return s.val;
            EvalueCaroBoard(ref b,_computer);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMaxNode());
            for(int i=0;i<_branch;i++)
            {
                b.cells[list[i].p.x, list[i].p.y] = _computer;
                alpha = Math.Max(alpha, s.val+MinVal(ref b, list[i], alpha, beta, depth + 1));
                b.cells[list[i].p.x, list[i].p.y] = ' ';
                if (alpha > beta) break;
            }
            return alpha;
        }
        private int MinVal(ref CaroBoard b, State s, int alpha, int beta, int depth)
        {
            if (s.val <= -KScore[4] || depth >=maxdepth) return s.val;
            EvalueCaroBoard(ref b, _player);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMinNode());
            for (int i = 0; i < _branch; i++)
            {
                b.cells[list[i].p.x, list[i].p.y] = _player;
                beta = Math.Min(beta, s.val+MaxVal(ref b, list[i], alpha, beta, depth + 1));
                b.cells[list[i].p.x, list[i].p.y] = ' ';
                if (alpha >= beta) break;
            }
            return beta;
        }
    }
}
