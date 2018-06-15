﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public delegate double DelFunc(double x);

    /// <summary>
    /// Поиск корней уравнения
    /// </summary>
    public static class HalfDif
    {

        /// <summary>
        /// Поиск корней уравнения Методом половинного деления
        /// </summary>
        public static double Calculate(double eps, double left, double right, DelFunc func)
        {
            var delta = right - left;
            var curDelta = delta;
            double Fmin = func(left);
            double Fmax = func(right);
            if (Fmin * Fmax > 0) return Double.NaN;
            while (curDelta > eps)
            {
                double x = (left + right) / 2;
                double Fx = func(x);
                if (Fmin * Fx < 0)
                {
                    right = x;
                }
                else
                {
                    left = x;
                    Fmin = Fx;
                }

                curDelta = (right - left);
            }
            return (left + right) / 2;
        }
    }
}
