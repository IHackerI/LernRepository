using System;

namespace FunctionsMath
{
	public class OperationFunc : IFunction
	{
		public string Name{ get{ return _name; }}

		string _name;

		IFunction _firstOperand;
		IFunction _secondOperand;
		FunctionBuilder.Operation _operation;

		public OperationFunc (IFunction firstOperand, IFunction secondOperand, FunctionBuilder.Operation operation )
		{
			_firstOperand = firstOperand;
			_secondOperand = secondOperand;
			_operation = operation;
		}

		public object GetValue(out FunctionType type)
		{
			FunctionType firstType;
			FunctionType secondType;

			var firstValue = _firstOperand.GetValue (out firstType);
			var secondValue = _secondOperand.GetValue (out secondType);
            
			if (firstType == FunctionType.Double || secondType == FunctionType.Double) {
				type = FunctionType.Double;

                var a = (firstType == FunctionType.Double ? (double)firstValue : (double)(long)firstValue);
				var b = (secondType == FunctionType.Double ? (double)secondValue : (double)(long)secondValue);
				switch(_operation)
				{
					case FunctionBuilder.Operation.Plus: return a+b;
					case FunctionBuilder.Operation.Multiply: return a*b;
					case FunctionBuilder.Operation.Minus: return a-b;
					case FunctionBuilder.Operation.DivMod: return a%b;
					case FunctionBuilder.Operation.Divide: return a/b;
					case FunctionBuilder.Operation.Power: return Math.Pow (a,b);
				}
			} else {
				type = FunctionType.Long;

				var a = (long)firstValue;
				var b = (long)secondValue;
				switch(_operation)
				{
					case FunctionBuilder.Operation.Plus: return a+b;
					case FunctionBuilder.Operation.Multiply: return a*b;
					case FunctionBuilder.Operation.Minus: return a-b;
					case FunctionBuilder.Operation.DivMod: return a%b;
					case FunctionBuilder.Operation.Divide: type = FunctionType.Double; return a/(double)b;
					case FunctionBuilder.Operation.Power: return Math.Pow (a,b);
				}
			}

			type = FunctionType.None;
			return null;
		}

		public override string ToString ()
		{
			return _firstOperand + " " + _operation + " " + _secondOperand + " OperationFunc";
		}

        public void Rename(string name)
        {
            _name = name;
        }
    }
}

