using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    internal class LinearExpression : IFunction
    {
        private readonly double m_k;
        private readonly double m_b;

        public LinearExpression(double k, double b)
        {
            m_k = k;
            m_b = b;

        }

        public float Calc(double x)
        {
            return (float)(m_k * x + m_b);
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
