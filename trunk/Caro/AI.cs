using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
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
    class AI
    {
        int n;
        Random rand;
        int[] TScore = { 0, 7, 9, 85, 769 };
        int[] TScoreC = { 0, 8, 9, 85, 769 };
        int[] KScore = { 0, 7, 28, 256, 2308 };
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
        public Position prevp, currp;
        void ResetVal()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Val[i, j] = 0;
        }
        public AI(int size, int ai)
        {
            n = size;
            rand = new Random();
            Val = new int[n, n];
            _branch = 3;
            maxdepth = ai;
            prevp = new Position(-1, -1);
            currp = new Position(-1, -1);
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
                                    else Val[rw, cl + i] += KScore[cPlayer];
                                    //                                     if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _computer && b.cells[rw, cl + 5] == _computer)
                                    //                                         Val[rw, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw, cl + i] += TScore[cComputer];
                                    else Val[rw, cl + i] += KScore[cComputer];
                                    //                                     if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == _player && b.cells[rw, cl + 5] == _player)
                                    //                                         Val[rw, cl + i] = 0;
                                }
                                //if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw, cl + i - 1) && b.cells[rw, cl + i - 1] == ' ') || (b.CheckPosition(rw, cl + i + 1) && b.cells[rw, cl + i + 1] == ' ')))
                                if (cComputer == 4 || cPlayer == 4)
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
                                    else Val[rw + i, cl] += KScore[cPlayer];
                                    //                                     if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _computer && b.cells[rw + 5, cl] == _computer)
                                    //                                         Val[rw + i, cl] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw + i, cl] += TScore[cComputer];
                                    else Val[rw + i, cl] += KScore[cComputer];
                                    //                                     if (b.CheckPosition(rw - 1, cl) && b.CheckPosition(rw + 5, cl) && b.cells[rw - 1, cl] == _player && b.cells[rw + 5, cl] == _player)
                                    //                                         Val[rw + i, cl] = 0;
                                }
                                //if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw + i - 1, cl) && b.cells[rw + i - 1, cl] == ' ') || (b.CheckPosition(rw + i + 1, cl) && b.cells[rw + i + 1, cl] == ' ')))
                                if (cComputer == 4 || cPlayer == 4)
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
                                    if (Player == _computer) Val[rw + i, cl + i] += TScoreC[cPlayer];
                                    else Val[rw + i, cl + i] += KScore[cPlayer];
                                    //                                     if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _computer && b.cells[rw + 5, cl + 5] == _computer)
                                    //                                         Val[rw + i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    if (Player == _player) Val[rw + i, cl + i] += TScoreC[cComputer];
                                    else Val[rw + i, cl + i] += KScore[cComputer];
                                    //                                     if (b.CheckPosition(rw - 1, cl - 1) && b.CheckPosition(rw + 5, cl + 5) && b.cells[rw - 1, cl - 1] == _player && b.cells[rw + 5, cl + 5] == _player)
                                    //                                         Val[rw + i, cl + i] = 0;
                                }
                                //if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw + i - 1, cl + i - 1) && b.cells[rw + i - 1, cl + i - 1] == ' ') || (b.CheckPosition(rw + i + 1, cl + i + 1) && b.cells[rw + i + 1, cl + i + 1] == ' ')))
                                if (cComputer == 4 || cPlayer == 4)
                                    Val[rw + i, cl + i] *= 2;
                            }
                }
            //Duong cheo len
            for (rw = 4; rw < n; rw++)
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
                                    //Val[rw + i, cl + i] += 2;
                                    if (Player == _computer) Val[rw - i, cl + i] += TScoreC[cPlayer];
                                    else Val[rw - i, cl + i] += KScore[cPlayer];
                                    //                                     if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _computer && b.cells[rw - 5, cl + 5] == _computer)
                                    //                                         Val[rw - i, cl + i] = 0;
                                }
                                if (cPlayer == 0)
                                {
                                    //Val[rw + i, cl + i] += 2;
                                    if (Player == _player) Val[rw - i, cl + i] += TScoreC[cComputer];
                                    else Val[rw - i, cl + i] += KScore[cComputer];
                                    //                                     if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == _player && b.cells[rw - 5, cl + 5] == _player)
                                    //                                         Val[rw + i, cl + i] = 0;
                                }
                                //if ((cComputer == 4 || cPlayer == 4) && ((b.CheckPosition(rw - i + 1, cl + i - 1) && b.cells[rw - i + 1, cl + i - 1] == ' ') || (b.CheckPosition(rw - i - 1, cl + i + 1) && b.cells[rw - i - 1, cl + i + 1] == ' ')))
                                if (cComputer == 4 || cPlayer == 4)
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

                    Console.Write("{0}{1}", Val[i, j], Space(Val[i, j]));
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
                Val[list[i].p.x, list[i].p.y] = 0;
                //Console.WriteLine("{0}-{1}", list[i].p.x, list[i].p.y);
            }
            //Console.WriteLine("----");
            int x = rand.Next(0, list.Count);
            return list[x];
        }
        private string[] Truonghopx = { @"\sxx\s", @"\sxxxo", @"oxxx\s", @"\sxxx\s", @"\sxxxxo", @"oxxxx\s", @"\sxxxx\s", @"xxxxx"};
        private string[] Truonghopo = { @"\soo\s", @"\sooox", @"xooo\s", @"\sooo\s", @"\soooox", @"xoooo\s", @"\soooo\s", @"ooooo"};
        private int[] point = { 6, 4, 4, 12, 30, 30, 3000, 10000 };
        private int Eval(ref CaroBoard b)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    s += b.cells[i, j];
                s += ";";
                for (int j = 0; j < n; j++)
                    s += b.cells[j, i];
                s += ";";
            }
            for (int i = 0; i < n - 4; i++)
            {
                for (int j = 0; j < n - i; j++)
                    s += b.cells[j, i + j];
                s += ";";
            }
            for (int i = n - 5; i > 0; i--)
            {
                for (int j = 0; j < n - i; j++)
                    s += b.cells[i + j, j];
                s += ";";
            }
            for (int i = 4; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                    s += b.cells[i - j, j];
                s += ";";
            }
            for (int i = n - 5; i > 0; i--)
            {
                for (int j = n - 1; j >= i; j--)
                    s += b.cells[j, i + n - j - 1];
                s += ";\n";
            }
            //Console.WriteLine(s);
            Regex regex1,regex2;
            int diem = 0;
            for (int i = 0; i < Truonghopx.Length; i++)
            {
                regex1 = new Regex(Truonghopx[i]);
                regex2 = new Regex(Truonghopo[i]);
                if (_computer == 'o')
                {
                    diem += point[i] * regex2.Matches(s).Count;
                    diem -= point[i] * regex1.Matches(s).Count;
                }
                else
                {
                    diem -= point[i] * regex2.Matches(s).Count;
                    diem += point[i] * regex1.Matches(s).Count;
                }
            }
            return diem;
        }
        public Position Solve(ref CaroBoard bb, char Player)
        {
            currp.Set(-1, -1);
            prevp.Set(-1, -1);
            CaroBoard b = new CaroBoard(bb.size);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    b.cells[i, j] = bb.cells[i, j];
            //Console.WriteLine("Current P={0}", Eval(ref b));
            computer = Player;
            EvalueCaroBoard(ref b, _computer);
            EchoVal();
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
            {
                list.Add(GetMaxNode());
                if (list[i].val > 1538) break;
            }
            int maxp = -INT_MAX;
            List<State> ListChoose = new List<State>();
            for (int i = 0; i < list.Count; i++)
            {
                currp.Set(list[i].p);
                b.cells[list[i].p.x, list[i].p.y] = _computer;
                int t = MinVal(ref b, list[i], -INT_MAX, INT_MAX, 0);
                Console.WriteLine("{0}-{1}: {2}", list[i].p.x, list[i].p.y, t);//list[i].val);
                if (maxp < t)
                {
                    maxp = t;
                    ListChoose.Clear();
                    ListChoose.Add(list[i]);
                }else if(maxp==t)
                {
                    ListChoose.Add(list[i]);
                }
                b.cells[list[i].p.x, list[i].p.y] = ' ';
            }
            int x = rand.Next(0, ListChoose.Count);
            //Console.Write("i={0};",x);
            return ListChoose[x].p;
        }
        private int MaxVal(ref CaroBoard b, State s, int alpha, int beta, int depth)
        {
            int val = Eval(ref b);
            if (depth >= maxdepth || Math.Abs(val) > 3000) return val;
            EvalueCaroBoard(ref b, _computer);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
            {
                list.Add(GetMaxNode());
                if (list[i].val > 1538) break;
            }
            for (int i = 0; i < list.Count; i++)
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
            int val = Eval(ref b);
            if (depth >= maxdepth || Math.Abs(val) > 3000) return val;
            EvalueCaroBoard(ref b, _player);
            List<State> list = new List<State>();
            for (int i = 0; i < _branch; i++)
            {
                list.Add(GetMaxNode());
                if (list[i].val > 1538) break;
            }
            for (int i = 0; i < list.Count; i++)
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
