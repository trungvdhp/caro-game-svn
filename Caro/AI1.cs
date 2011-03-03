using System;
using System.Collections.Generic;
using System.Text;

namespace Caro
{
    class AI1
    {
        int n;
        int[] TScore = { 0, 1, 9, 85, 769 };
        int[] KScore = { 0, 2, 28, 256, 2308 };
        int[,] Val;
        void ResetVal()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Val[i, j] = 0;
        }
        public AI1(int size)
        {
            n = size;
            Val = new int[n, n];
        }
        public void EvalueCaroBoard(ref CaroBoard b,char Player)
        {
            n=b.size;
            ResetVal();
            int rw, cl, i;
            int cX, cO;
            //Luong gia cho hang
            for(rw=0;rw<n;rw++)
                for(cl=0;cl<n-4;cl++)
                {
                    cX = 0; cO = 0;
                    for(i=0;i<5;i++)
                    {
                        if (b.cells[rw, cl + i] == 'x') cX++;
                        if (b.cells[rw, cl + i] == 'o') cO++;
                    }
                    //Luong gia..
                    if(cX*cO==0&&cX!=cO)
                        for(i=0;i<5;i++)
                            if(b.cells[rw,cl+i]==' ')
                            {
                                if (cX == 0)
                                {
                                    if (Player == 'x') Val[rw, cl + i] += TScore[cO];
                                    else Val[rw, cl + i] += KScore[cO];
                                    if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == 'x' && b.cells[rw, cl + 5] == 'x')
                                        Val[rw, cl + i] = 0;
                                }
                                if (cO == 0)
                                {
                                    if (Player == 'o') Val[rw, cl + i] += TScore[cX];
                                    else Val[rw, cl + i] += KScore[cX];
                                    if (b.CheckPosition(rw, cl - 1) && b.CheckPosition(rw, cl + 5) && b.cells[rw, cl - 1] == 'o' && b.cells[rw, cl + 5] == 'o')
                                        Val[rw, cl + i] = 0;
                                }
//                                 if ((cX == 4 || cO == 4) && ((b.CheckPosition(rw, cl + i - 1) && b.cells[rw, cl + i - 1] == ' ') || b.cells[rw, cl + i + 1] == ' '))
//                                     Val[rw, cl + i] *= 2;
                            }
                }
            //Luong gia cho cot
            for (rw = 0; rw < n-4; rw++)
                for (cl = 0; cl < n; cl++)
                {
                    cX = 0; cO = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw+i, cl] == 'x') cX++;
                        if (b.cells[rw+i, cl] == 'o') cO++;
                    }
                    //Luong gia..
                    if (cX * cO == 0 && cX != cO)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw+i, cl] == ' ')
                            {
                                if (cX == 0)
                                {
                                    if (Player == 'x') Val[rw+i, cl] += TScore[cO];
                                    else Val[rw+i, cl] += KScore[cO];
                                    if (b.CheckPosition(rw-1, cl) && b.CheckPosition(rw+5, cl) && b.cells[rw-1, cl] == 'x' && b.cells[rw+5, cl] == 'x')
                                        Val[rw+i, cl] = 0;
                                }
                                if (cO == 0)
                                {
                                    if (Player == 'o') Val[rw+i, cl] += TScore[cX];
                                    else Val[rw+i, cl] += KScore[cX];
                                    if (b.CheckPosition(rw-1, cl) && b.CheckPosition(rw+5, cl) && b.cells[rw-1, cl] == 'o' && b.cells[rw+5, cl] == 'o')
                                        Val[rw+i, cl] = 0;
                                }
//                                 if ((cX == 4 || cO == 4) && ((b.CheckPosition(rw+i-1,cl)&&b.cells[rw+i-1, cl] == ' ') || b.cells[rw+i+1, cl] == ' '))
//                                     Val[rw+i, cl] *= 2;
                            }
                }
            //Duong cheo xuong
            for (rw = 0; rw < n - 4; rw++)
                for (cl = 0; cl < n-4; cl++)
                {
                    cX = 0; cO = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw + i, cl+i] == 'x') cX++;
                        if (b.cells[rw + i, cl+i] == 'o') cO++;
                    }
                    //Luong gia..
                    if (cX * cO == 0 && cX != cO)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw + i, cl+i] == ' ')
                            {
                                if (cX == 0)
                                {
                                    if (Player == 'x') Val[rw + i, cl+i] += TScore[cO];
                                    else Val[rw + i, cl+i] += KScore[cO];
                                    if (b.CheckPosition(rw - 1, cl-1) && b.CheckPosition(rw + 5, cl+5) && b.cells[rw - 1, cl-1] == 'x' && b.cells[rw + 5, cl+5] == 'x')
                                        Val[rw + i, cl+i] = 0;
                                }
                                if (cO == 0)
                                {
                                    if (Player == 'o') Val[rw + i, cl+i] += TScore[cX];
                                    else Val[rw + i, cl+i] += KScore[cX];
                                    if (b.CheckPosition(rw - 1, cl-1) && b.CheckPosition(rw + 5, cl+5) && b.cells[rw - 1, cl -1] == 'o' && b.cells[rw + 5, cl+5] == 'o')
                                        Val[rw + i, cl+i] = 0;
                                }
//                                 if ((cX == 4 || cO == 4) && ((b.CheckPosition(rw + i - 1, cl+i-1) && b.cells[rw + i - 1, cl+i-1] == ' ') || (b.CheckPosition(rw + i + 1, cl+i+1) && b.cells[rw + i + 1, cl+i+1] == ' ')))
//                                     Val[rw + i, cl+i] *= 2;
                            }
                }
            //Duong cheo len
            for (rw = 4; rw < n - 4; rw++)
                for (cl = 0; cl < n-4 ; cl++)
                {
                    cX = 0; cO = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (b.cells[rw - i, cl + i] == 'x') cX++;
                        if (b.cells[rw - i, cl + i] == 'o') cO++;
                    }
                    //Luong gia..
                    if (cX * cO == 0 && cX != cO)
                        for (i = 0; i < 5; i++)
                            if (b.cells[rw - i, cl + i] == ' ')
                            {
                                if (cX == 0)
                                {
                                    if (Player == 'x') Val[rw - i, cl + i] += TScore[cO];
                                    else Val[rw - i, cl + i] += KScore[cO];
                                    if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == 'x' && b.cells[rw - 5, cl + 5] == 'x')
                                        Val[rw - i, cl + i] = 0;
                                }
                                if (cO == 0)
                                {
                                    if (Player == 'o') Val[rw - i, cl + i] += TScore[cX];
                                    else Val[rw - i, cl + i] += KScore[cX];
                                    if (b.CheckPosition(rw + 1, cl - 1) && b.CheckPosition(rw - 5, cl + 5) && b.cells[rw + 1, cl - 1] == 'o' && b.cells[rw - 5, cl + 5] == 'o')
                                        Val[rw + i, cl + i] = 0;
                                }
//                                 if ((cX == 4 || cO == 4) && ((b.CheckPosition(rw - i + 1, cl + i - 1) && b.cells[rw - i + 1, cl + i - 1] == ' ') || (b.CheckPosition(rw - i - 1, cl + i + 1) && b.cells[rw - i - 1, cl + i + 1] == ' ')))
//                                     Val[rw - i, cl + i] *= 2;
                            }
                }
            EchoVal();

        }
        void EchoVal()
        {
            Console.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    
                    Console.Write("{0}{1}",Val[i, j],Space(Val[i,j]));
                }
                Console.WriteLine();
            }
        }
        string Space(int x)
        {
            int k = x.ToString().Length;
            string r = "";
            for (int i = 0; i < 4-k; i++)
                r += " ";
            return r;
        }
        public Position Solve(ref CaroBoard b, char Player )
        {
            EvalueCaroBoard(ref b, Player);
            int MaxP=0;
            Position p = new Position(b.size / 2, b.size / 2);
            for(int i=0;i<n;i++)
                for(int j=0;j<n;j++)
                {
                    if(MaxP<Val[i,j])
                    {
                        MaxP = Val[i, j];
                        p.Set(i, j);
                    }
                }
            return p;
        }

    }
}
