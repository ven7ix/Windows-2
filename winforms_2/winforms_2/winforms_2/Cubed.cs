using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    internal class Cubed : IFunction
    {
        public float Calc(double x)
        {
            return (float)(x * x * x);
        }

        public float Derivative(double x)
        {
            float xDelta = 0.0001f;
            float derivative;

            derivative = (Calc(x + xDelta) - Calc(x)) / xDelta;

            return derivative;
        }

        public float CalcX(double y)
        {
            return (float)Math.Sqrt(y);
        }
    }
}
