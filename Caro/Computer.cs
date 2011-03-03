using System;
using System.Collections.Generic;
using System.Text;

namespace Caro
{
    class Computer
    {
        int[,] Val;
        int n;
        public Computer(int size)
        {
            this.n = size;
            Val = new int[n, n];
            ResetVal();
        }
        void ResetVal()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Val[i, j] = 0;
        }

        int GetPoint()
        {

            return 0;
        }
    }
}
