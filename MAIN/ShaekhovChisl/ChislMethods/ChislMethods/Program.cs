using ChislMethods.FindFuncs;
using ChislMethods.LinAl;
using ChislMethods.Spline;
using ChislMethods.DerSystems;
using ChislMethods.Integral;

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
            DerSystemsTEST.TEST();
            IntegralTEST.TEST();
        }
    }
}
