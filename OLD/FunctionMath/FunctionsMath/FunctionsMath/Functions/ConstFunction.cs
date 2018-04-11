using System;
using System.Globalization;

namespace FunctionsMath
{
	public class ConstFunction : IFunction
	{
		public string Name{ get{ return _name; }}

		string _name;

		object _value;
		FunctionType _type;

		public ConstFunction (object value, FunctionType type)
		{
			_value = value;
			_type = type;
			_name = _type+" const";
		}

		public object GetValue(out FunctionType type)
		{
			type = _type;
			return _value;
		}

		public override string ToString ()
		{
			return _value + " ConstFunc";
		}

        public void Rename(string name)
        {
            _name = name;
        }

        public static bool TryParse(string value, out ConstFunction func)
        {
            long outL;

            if (long.TryParse(value, out outL))
            {
                func = new ConstFunction(outL, FunctionType.Long);
                return true;
            }

            double outD;

			value = value.Replace ('.', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);
			value = value.Replace (',', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);

			if (double.TryParse(value, out outD))
			{
                func = new ConstFunction(outD, FunctionType.Double);
                return true;
            }

            func = null;
            return false;
        }
    }
}