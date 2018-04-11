using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsMath
{
    public class ArgumentableFunctionsBase : IFunction
    {
        public string Name { get { return _name; } }
        string _name;

        IFunction[] _argsFuncs;
        string[] _argsNames;

        //Dictionary<string, IFunction> argsFuncs = new Dictionary<string, IFunction>();
        public IFunction CalcFunction { get { return _calcFunction; } set { if (_calcFunction != null) throw new Exception(); _calcFunction = value; } }
        IFunction _calcFunction;

        public ArgumentableFunctionsBase(string name, IFunction func, string[] argsNames)
        {
            _argsFuncs = new IFunction[argsNames.Length];
            _argsNames = argsNames;

            _calcFunction = func;

            _name = name;
        }

        public IFunction GetArgumentFunction(string argName)
        {
            return _argsFuncs[Array.IndexOf(_argsNames, argName)];
        }

        public void SetArgumentFunction(int argIndex, IFunction func)
        {
            _argsFuncs[argIndex] = func;
        }

        public object GetValue(out FunctionType type)
        {
            var ans = _calcFunction.GetValue(out type);

            Array.Clear(_argsFuncs, 0, _argsFuncs.Length);
            return ans;
        }

        public override string ToString()
        {
            return Name + " ArgFuncBase";
        }

        public void Rename(string name)
        {
            _calcFunction.Rename(name);
            _name = name;
        }
    }
}
