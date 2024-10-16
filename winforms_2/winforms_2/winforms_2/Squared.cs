using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    internal class Squared : IFunction
    {
        public float Calc(double x)
        {
            return (float)(x * x);
        }
    }
}
