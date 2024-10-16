using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    //сделать что-то с тангенсом, чтобы не было вертикальных линий
    internal class Tangent : IFunction
    {
        public float Calc(double x)
        {
            return (float)Math.Tan(x);
        }
    }
}
