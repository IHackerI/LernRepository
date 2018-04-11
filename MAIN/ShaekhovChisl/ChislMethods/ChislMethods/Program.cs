using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChislMethods.FindFuncs;
using ChislMethods.LinAl;
using ChislMethods.Spline;

namespace ChislMethods
{
    public delegate double DelF(double x);

    class Program
    {
        static void Main(string[] args)
        {
            FincValueFindersTest.TEST();
            LinAlTester.TEST();
            SplineTest.TEST();
        }
    }
}
