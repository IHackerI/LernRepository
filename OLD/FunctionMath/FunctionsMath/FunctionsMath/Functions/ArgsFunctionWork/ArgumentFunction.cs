using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsMath
{
    class ArgumentFunction : IFunction
    {
        public string Name { get { return _name;  } }
        private string _name;
        private ArgumentableFunctionsBase _baseFunction;

        public ArgumentFunction(string name, ArgumentableFunctionsBase baseFunction)
        {
            _baseFunction = baseFunction;
            _name = name;
        }

        public object GetValue(out FunctionType type)
        {
            return _baseFunction.GetArgumentFunction(_name).GetValue(out type);
        }

        public void Rename(string name)
        {
            _name = name;
        }
    }
}
