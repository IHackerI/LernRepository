using System;

namespace FunctionsMath
{
	public class NonargumentableFunction: IFunction
	{
		public string Name{ get{ return _name; }}

		string _name;
        IFunction _function;


        public NonargumentableFunction (IFunction function)
		{
            _function = function;
            _name = null;
		}

		public object GetValue(out FunctionType type)
		{
			return _function.GetValue(out type);
		}

		public override string ToString ()
		{
			return Name + " NonargFunc";
		}

        public void Rename(string name)
        {
            _name = name;
        }
    }
}

