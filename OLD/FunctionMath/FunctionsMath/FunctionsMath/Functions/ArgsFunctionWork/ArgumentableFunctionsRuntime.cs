using System;
using System.Collections;

namespace FunctionsMath
{
	public class ArgumentableFunctionsRuntime : IFunction
	{
		public string Name{ get{ return _name; }}

        private string _name;

        ArgumentableFunctionsBase _baseFunc;
        IFunction[] _argFunctions;

        public ArgumentableFunctionsRuntime (ArgumentableFunctionsBase baseFunc,  IFunction[] functions)
		{
            _name = null;
            _baseFunc = baseFunc;
            _argFunctions = functions;
        }

		public object GetValue(out FunctionType type)
		{
            for (int i = 0; i < _argFunctions.Length; i++)
            {
                _baseFunc.SetArgumentFunction(i, _argFunctions[i]);
            }
            
            return _baseFunc.GetValue(out type);
		}

		public override string ToString ()
		{
			return _baseFunc + "Runtime";
		}

        public void Rename(string name)
        {
            _name = name;
        }
    }
}

