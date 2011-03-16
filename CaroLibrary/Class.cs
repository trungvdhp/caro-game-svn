using System;
using System.Collections.Generic;
using System.Text;

namespace CaroLibrary
{
    public class Position
    {
        public int x;
        public int y;
        public Position()
        {
        }
        public void Set(Position p)
        {
            x = p.x; y = p.y;
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
    public class Step
    {
        public char CurrentPlayer;
        public Position p;
        public Step()
        {
            p = new Position(-1, -1);
            CurrentPlayer = ' ';
        }
        public Step(Position pp, char cc)
        {
            CurrentPlayer = cc;
            p = new Position();
            p.Set(pp);
        }
    }
}
