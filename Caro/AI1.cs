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
        public State(Position p, int val)
        {
            this.p = new Position();
            this.p.x = p.x;
            this.p.y = p.y;
            this.val = val;
        }
        public void Set(Position p, int val)
        {
            this.p = new Position();
            this.p.x = p.x;
            this.p.y = p.y;
            this.val = val;
        }
    }
    class AI1
    {
        int n;
        //int[] TScore = { 0, 1, 9, 85, 769 };
        //int[] KScore = { 0, 2, 28, 256, 2308 };
        int[] TScore = { 0, 1, 9, 85, 769,2308 };
        int[] KScore = { 0, 2, 28, 256, 2308 };
        int[,] Val;
        private int maxdepth;
        char _computer;
        char _player;
        int _branch;
        const int INT_MAX = 2147483647;
        public char computer
        {
            get
            {
                return _computer;
            }
            set
            {
                if (value == 'x')
                    _player = 'o';
                else
                    _player = 'x';
                _computer = value;
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
            _branch = 3;
            maxdepth = 6;
        }
        //private int Eval(ref CaroBoard b,char Player)
        //{
        //    ResetVal();
        //    int n = b.size;
        //    int rw, cl, i,j, cComputer, cPlayer;
        //    for(rw=0;rw<n;rw++)
        //        for(cl=0;cl<n;cl++)
        //        {
        //            if(b.cells[rw,cl]==_computer)
        //            {
        //                for(i=-4;i<=0;i++)
        //                {
        //                    //ngang
        //                    cComputer = cPlayer = 0;
        //                    for(j=0;j<5;j++)
        //                    {
        //                        if (b.CheckPosition(rw, cl + i + j) && b.cells[rw, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw, cl + i + j) && b.cells[rw, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for(j=0;j<5;j++)
        //                    {
        //                        if (cComputer == 1) Val[rw, cl] += TScore[cPlayer+1];
        //                        else if(cPlayer==0) Val[rw, cl] += KScore[cComputer-1];
        //                        if (cComputer == 5 || cPlayer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    //doc
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw + i + j, cl) && b.cells[rw + i + j, cl] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw + i + j, cl) && b.cells[rw + i + j, cl] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cComputer == 1) Val[rw, cl] += TScore[cPlayer + 1];
        //                        else if (cPlayer == 0) Val[rw, cl] += KScore[cComputer - 1];
        //                        if (cComputer == 5 || cPlayer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw + i + j, cl + i + j) && b.cells[rw + i + j, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw + i + j, cl + i + j) && b.cells[rw + i + j, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cComputer == 1) Val[rw, cl] += TScore[cPlayer + 1];
        //                        else if (cPlayer == 0) Val[rw, cl] += KScore[cComputer - 1];
        //                        if (cComputer == 5 || cPlayer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw-i-j, cl + i + j) && b.cells[rw-i-j, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw - i - j, cl + i + j) && b.cells[rw-i-j, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cComputer == 1) Val[rw, cl] += TScore[cPlayer + 1];
        //                        else if (cPlayer == 0) Val[rw, cl] += KScore[cComputer - 1];
        //                        if (cComputer == 5 || cPlayer == 4) Val[rw, cl] *= 2;
        //                    }
        //                }
        //            }
        //            if(b.cells[rw,cl]==_player)
        //            {
        //                for(i=-4;i<=0;i++)
        //                {
        //                    //ngang
        //                    cComputer = cPlayer = 0;
        //                    for(j=0;j<5;j++)
        //                    {
        //                        if (b.CheckPosition(rw, cl + i + j) && b.cells[rw, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw, cl + i + j) && b.cells[rw, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for(j=0;j<5;j++)
        //                    {
        //                        if (cPlayer == 1) Val[rw, cl] -= TScore[cComputer + 1];
        //                        else if (cComputer == 0) Val[rw, cl] -= KScore[cPlayer - 1];
        //                        if (cPlayer == 5 || cComputer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    //doc
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw + i + j, cl) && b.cells[rw + i + j, cl] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw + i + j, cl) && b.cells[rw + i + j, cl] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cPlayer == 1) Val[rw, cl] -= TScore[cComputer + 1];
        //                        else if (cComputer == 0) Val[rw, cl] -= KScore[cPlayer - 1];
        //                        if (cPlayer == 5 || cComputer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw + i + j, cl + i + j) && b.cells[rw + i + j, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw + i + j, cl + i + j) && b.cells[rw + i + j, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cPlayer == 1) Val[rw, cl] -= TScore[cComputer + 1];
        //                        else if (cComputer == 0) Val[rw, cl] -= KScore[cPlayer - 1];
        //                        if (cPlayer == 5 || cComputer == 4) Val[rw, cl] *= 2;
        //                    }
        //                    cComputer = cPlayer = 0;
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (b.CheckPosition(rw-i-j, cl + i + j) && b.cells[rw-i-j, cl + i + j] == _computer) cComputer++;
        //                        if (b.CheckPosition(rw - i - j, cl + i + j) && b.cells[rw-i-j, cl + i + j] == _player) cPlayer++;
        //                    }
        //                    for (j = 0; j < 5; j++)
        //                    {
        //                        if (cPlayer == 1) Val[rw, cl] -= TScore[cComputer + 1];
        //                        else if (cComputer == 0) Val[rw, cl] -= KScore[cPlayer - 1];
        //                        if (cPlayer == 5 || cComputer == 4) Val[rw, cl] *= 2;
        //                    }
        //                }
        //            }
        //        }
        //    int result=0;
        //    for (rw = 0; rw < n; rw++)
        //        for (cl = 0; cl < n; cl++)
        //            result += Val[rw, cl];
        //    //EchoVal();
        //    return result;
        //}
        private int Eval1(ref CaroBoard b, char Player)
        {
            int cComputer = 0, cPlayer = 0;
            for(int i=0;i<b.size;i++)
                for(int j=0;j<b.size;j++)
                {
                    for (int i1 = i - 1; i1 <= i + 1; i1++)
                        for (int j1 = j - 1; j1 <= j+1; j1++)
                            if (b.CheckPosition(i1, j1) && b.cells[i1, j1] == _computer)
                                cComputer++;
                            else if (b.CheckPosition(i1, j1) && b.cells[i1, j1] == _player)
                                cPlayer++;
                }
            if (Player == _computer) return cComputer - cPlayer - 1;
            return cPlayer - cComputer - 1;
        }
        public void EvalueCaroBoard(ref CaroBoard b, char Player)
        {
            n = b.size;
            ResetVal();
            int rw, cl, i;
            int cComputer, cPlayer;
            //kiem tra hang
            for (rw = 0; rw < n; rw++)
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw, cl + i] == _computer) cComputer++;
                        if (b.cells[rw, cl + i] == _player) cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw, cl + i] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw, cl + i] += TScore[cPlayer];
                                    else Val[rw, cl + i] -= KScore[cPlayer];
                                    if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _computer && b.cells[rw, cl + 5] == _computer)
                                        Val[rw, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw, cl + i] -= TScore[cComputer];
                                    else Val[rw, cl + i] += KScore[cComputer];
                                    if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _player && b.cells[rw, cl + 5] == _player)
                                        Val[rw, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw, cl + i - 1) && b.cells[rw, cl + i - 1] == ' ') || (b.CheckPosition(rw, cl + i + 1) && b.cells[rw, cl + i + 1] == ' ')))
                                    Val[rw, cl + i] *= 2;
                            }
                }
            //Cot
            for (rw = 0; rw < n - 4; rw++)
                for (cl = 0; cl < n; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw + i, cl] == _computer) cComputer++;
                        if (b.cells[rw + i, cl] == _player) cPlayer++;
                    }
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw + i, cl] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw + i, cl] += TScore[cPlayer];
                                    else Val[rw + i, cl] -= KScore[cPlayer];
                                    if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _computer && b.cells[rw + 5, cl] == _computer)
                                        Val[rw + i, cl] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw + i, cl] -= TScore[cComputer];
                                    else Val[rw + i, cl] += KScore[cComputer];
                                    if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _player && b.cells[rw + 5, cl] == _player)
                                        Val[rw + i, cl] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw + i - 1, cl) && b.cells[rw + i - 1, cl] == ' ') || (b.CheckPosition(rw + i + 1, cl) && b.cells[rw + i + 1, cl] == ' ')))
                                    Val[rw + i, cl] *= 2;
                            }
                }
            //Duong cheo xuong
            for (rw = 0; rw < n - 4; rw++)
                for (cl = 0; cl < n - 4; cl++)
                {
                    cComputer = 0; cPlayer = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw + i, cl + i] == _computer) cComputer++;
                        if (b.cells[rw + i, cl + i] == _player) cPlayer++;
                    }
                    //Luong gia..
                    if (cComputer * cPlayer == 0 && cComputer != cPlayer)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw + i, cl + i] == ' ')
                            {
                                if (cComputer == 0)
                                {
                                    if (Player == _computer) Val[rw + i, cl + i] += TScore[cPlayer];
                                    else Val[rw + i, cl + i] -= KScore[cPlayer];
                                    if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _computer && b.cells[rw + 5, cl + 5] == _computer)
                                        Val[rw + i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw + i, cl + i] -= TScore[cComputer];
                                    else Val[rw + i, cl + i] += KScore[cComputer];
                                    if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _player && b.cells[rw + 5, cl + 5] == _player)
                                        Val[rw + i, cl + i] = 0;
                                }
                                if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw + i - 1, cl + i - 1) && b.cells[rw + i - 1, cl + i - 1] == ' ') || (b.CheckPosition(rw + i + 1, cl + i + 1) && b.cells[rw + i + 1, cl + i + 1] == ' ')))
                                    Val[rw + i, cl + i] *= 2;
                            }
                }
            //Duong cheo len
            for (rw = 4; rw < n - 4; rw++)
                for (cl = 0; cl < n - 4; cl++)
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
                                    if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _computer && b.cells[rw - 5, cl + 5] == _computer)
                                        Val[rw - i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw - i, cl + i] -= TScore[cComputer];
                                    else Val[rw - i, cl + i] += KScore[cComputer];
                                    if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _player && b.cells[rw - 5, cl + 5] == _player)
                                        Val[rw + i, cl + i] = 0;
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

                    Console.Write("{0} ", Val[i, j]);
                }
                Console.WriteLine("\n");
            }
        }
        string Space(int x)
        {
            int k = x.ToString().Length;
            string r = "";
            //if (x < 0) k += 1;
            for (int i = 0; i < 4 - k; i++)
                r += " ";
            return r;
        }
        private State GetMaxNode()
        {
            Position p = new Position(0, 0);
            List<State> list = new List<State>();
            int t = -INT_MAX;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (t < Val[i, j])
                    {
                        t = Val[i, j];
                        p.Set(i, j);
                        list.Clear();
                        list.Add(new State(p, t));
                    }
                    else if (t == Val[i, j])
                    {
                        p.Set(i, j);
                        list.Add(new State(p, t));
                    }
                }
            for (int i = 0; i < list.Count; i++)
            {
                //Console.WriteLine("{0}-{1}:{2}", list[i].p.x, list[i].p.y, i);
                Val[list[i].p.x, list[i].p.y] = 0;
            }
            int r = new Random().Next(0, list.Count);
            //Console.Clear();
            //Console.WriteLine("{0}-{1}:{2}", list[r].p.x,list[r].p.y,r);
            return list[r];
        }
        private State GetMinNode()
        {
            Position p = new Position(0, 0);
            List<State> list = new List<State>();
            int t = INT_MAX;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (t > Val[i, j])
                    {
                        t = Val[i, j];
                        p.Set(i, j);
                        list.Clear();
                        list.Add(new State(p, t));
                    }
                    else if (t == Val[i, j])
                    {
                        p.Set(i, j);
                        list.Add(new State(p, t));
                    }
                }
            for (int i = 0; i < list.Count; i++)
                Val[list[i].p.x, list[i].p.y] = 0;
            int r = new Random().Next(0, list.Count);
            return list[r];
        }
        public Position Solve(ref CaroBoard b, char Player)
        {
            EvalueCaroBoard(ref b, Player);
            Position p = new Position(b.size / 2, b.size / 2);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMaxNode());
            int maxp = -INT_MAX;

            for (int i = 0; i < _branch; i++)
            {
                b.cells[list[i].p.x, list[i].p.y] = _computer;
                int t = MinVal(ref b, list[i], -INT_MAX, INT_MAX, 0);
                if (maxp < t)
                {
                    maxp = t;
                    p.Set(list[i].p);
                }
                b.cells[list[i].p.x, list[i].p.y] = ' ';
            }
            //p.Set(list[0].p);
            return p;
        }
        private int MaxVal(ref CaroBoard b, State s, int alpha, int beta, int depth)
        {
            if (depth >= maxdepth) return Eval1(ref b,_computer);
            EvalueCaroBoard(ref b, _computer);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMaxNode());
            for (int i = 0; i < _branch; i++)
            {
                b.cells[list[i].p.x, list[i].p.y] = _computer;
                alpha = Math.Max(alpha, MinVal(ref b, list[i], alpha, beta, depth + 1));
                b.cells[list[i].p.x, list[i].p.y] = ' ';
                if (alpha > beta) break;
            }
            return alpha;
        }
        private int MinVal(ref CaroBoard b, State s, int alpha, int beta, int depth)
        {
            if (depth >= maxdepth) return Eval1(ref b,_player);
            EvalueCaroBoard(ref b, _player);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
                list.Add(GetMinNode());
            for (int i = 0; i < _branch; i++)
            {
                b.cells[list[i].p.x, list[i].p.y] = _player;
                beta = Math.Min(beta, MaxVal(ref b, list[i], alpha, beta, depth + 1));
                b.cells[list[i].p.x, list[i].p.y] = ' ';
                if (alpha >= beta) break;
            }
            return beta;
        }
    }
}
