using System;
using System.Collections.Generic;

namespace FunctionsMath
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var Dictionary = new Dictionary<string, IFunction> ();

            string[] input = new string[]
            {
                "x = 5+5.5", 
                "A(x,Y) = 50+60+x*Y",
                "A(10,10) + 10+3+0.1",
                "x =1 + x",
                "c=x+3",
                "summ(x,y,z) = x+y+z",
                "summ(14,30,65)",
                "summ(1,2,3)"
            };

            for (int i = 0; i < input.Length; i++) {

                FunctionBuilder.ErrorSyntax error;
                var func = FunctionBuilder.GenerateFunction(input[i], Dictionary, out error);
                if (func.Name != null)
                {
                    if (Dictionary.ContainsKey(func.Name))
                    {
                        Dictionary[func.Name] = func;
                    } else 
                        Dictionary.Add(func.Name, func);
                }

                FunctionType outType;
                if (! (func is ArgumentableFunctionsBase))
                    Console.WriteLine(func.GetValue(out outType) + "\t" + outType);
                Console.WriteLine();
            }

            Console.ReadLine();
		}
	}
}
