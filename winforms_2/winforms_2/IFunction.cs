﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_2
{
    internal interface IFunction
    {
        //Calc можно использовать ТОЛЬКО на Graph точках, на точках Window - нельзя
        float Calc(double x);
        float Derivative(double x);
        float CalcX(double y);
    }
}
