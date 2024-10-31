using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    internal class Sine : IFunction
    {
        public float Calc(double x)
        {
            return (float)Math.Sin(x);
        }
    }
}
