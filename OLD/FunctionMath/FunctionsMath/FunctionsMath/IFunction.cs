using System;

namespace FunctionsMath
{
	public enum FunctionType{
		None = 0,
		Long = 10,
		Double = 20
	}

	public interface IFunction
	{
		string Name{ get;}

		object GetValue(out FunctionType type);

        void Rename(string name);
        /*

		Operation _operation;

		MathFunction _firstFunction;
		MathFunction _secondFunction;

		public void SetFunction(string src){
			
		}*/
	}
}

