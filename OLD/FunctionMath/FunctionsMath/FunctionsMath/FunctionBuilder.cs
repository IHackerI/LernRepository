using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace FunctionsMath
{
	public static class FunctionBuilder
	{
		const string Operators = "+-*/^%=()><,;";
		const string Numbers = "0123456789.";
		const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

		public enum Operation{
			None = -1,
			Plus = 0,
			Minus = 1,
			Multiply = 2,
			Divide = 3,
			Power = 4,
			DivMod = 5,
			Equal = 6,
			OpenBracket = 7,
			CloseBracket = 8,
			LeftMore = 9,
			RightMore = 10,
            ArgsSeparator1 = 11,
            ArgsSeparator2 = 12
        }

		public enum ErrorSyntax{
			None = 0,
            SourceIsEmpty = 10,
            NotFoundOpenBracket = 20,
            NotFoundCloseBracket = 30,
            ArgumentNotFound = 40,
            ObjectIsNotIFunction = 50,
            FirstSymbolIsOperation = 60,
            LastSymbolIsOperation = 70,
            EmptyArgsForFunction = 80,
            InvalidArgsStruct = 90,
            FunctionIsNotCollapsed = 100
        }

		public static IFunction GenerateFunction(string src, IDictionary<string, IFunction> knownVars, out ErrorSyntax error)
		{
			error = ErrorSyntax.None;

            #region ClearSrc
            if (EmptySrc(src, out error))
                return null;

            src = new string((from ch in src where (Operators.Contains(ch) || Numbers.Contains(ch) || Letters.Contains(ch)) select ch).ToArray());

            if (EmptySrc(src, out error))
                return null;
            #endregion

            #region SeparateSrcInArgs
            ArrayList args = new ArrayList();
			int startArgIndex = 0;

			for (int i = 0; i < src.Length; i++){
				int operatorIndex = Operators.IndexOf (src[i]);

				if (operatorIndex > -1){
                    
					if (startArgIndex != i) {

						if (args.Count > 0 && args [args.Count - 1].Equals (Operation.CloseBracket))
							args.Add (Operation.Multiply);
						
						args.Add (src.Substring (startArgIndex, i - startArgIndex));

						if ((Operation)operatorIndex == Operation.OpenBracket)
							args.Add (Operation.Multiply);
					} else {
						if ((Operation)operatorIndex == Operation.OpenBracket && args.Count > 0 && args [args.Count - 1].Equals (Operation.CloseBracket))
							args.Add (Operation.Multiply);
					}

					args.Add ((Operation)operatorIndex);

					startArgIndex = i+1;

					continue;
				}

                if (i == src.Length - 1) {
					i++;

					if (args.Count > 0 && args[args.Count-1].Equals(Operation.CloseBracket))
						args.Add (Operation.Multiply);
					args.Add (src.Substring(startArgIndex, i-startArgIndex));
					break;
				}
			}
            #endregion

            #region FindSimpleFunctions
            for (int i = args.Count-1; i > -1; i--){
				if (args[i] is string)
				{
					var name = (string)args [i];

					ConstFunction cFunc;

                    if (ConstFunction.TryParse(name, out cFunc))
                    {
                        args[i] = cFunc;
                        continue;
                    }

                    IFunction func;
					var knownFunc = knownVars.TryGetValue (name, out func);

					if (knownFunc) {
						if (func is ArgumentableFunctionsBase) {
							int closeIndex = FindCloseBracket (args, i + 2);

                            CollapseArgumentFunctions(args, i + 2, ref closeIndex, out error);
                            if (error != ErrorSyntax.None)
                                return null;

                            var Range = args.GetRange(i + 3, closeIndex - i - 3).ToArray();

                            var no = new ArgumentableFunctionsRuntime((ArgumentableFunctionsBase)func, (from x in Range select (IFunction)x).ToArray());

							args.RemoveRange (i, closeIndex + 1 - i);
							args.Insert (i, no);
						}

						if (func is NonargumentableFunction){
							args [i] = func;
						}
					}
				}
			}
            #endregion

            #region SetFuncsArgs
            if (args.Count > 2 && args[0] is string && args[1].Equals(Operation.Multiply) && args[2].Equals(Operation.OpenBracket))
            {
                int closeBracketInd = FindCloseBracket(args, 2);

                if (closeBracketInd < 0)
                {
                    error = ErrorSyntax.NotFoundCloseBracket;
                    return null;
                }

                string[] funcArgsNames = new string[(closeBracketInd - 2) >> 1];

                if (funcArgsNames.Length < 1)
                {
                    error = ErrorSyntax.EmptyArgsForFunction;
                    return null;
                }

                int counter = 0;

                for (int i = 3; i < closeBracketInd; i += 2)
                {
                    if (!(args[i] is string || args[i] is IFunction))
                    {
                        error = ErrorSyntax.InvalidArgsStruct;
                        return null;
                    }

                    funcArgsNames[counter] = /*(string)*/(args[i] is string ? (string)args[i] : ((IFunction)args[i]).Name);
                    counter++;
                }

                var argumentableFunc = new ArgumentableFunctionsBase((string)args[0], null, funcArgsNames);

                foreach (var argName in funcArgsNames)
                {
                    var argFunc = new ArgumentFunction(argName, argumentableFunc);

                    for (int i = 0; i < args.Count; i++)
                    {
                        if (args[i].Equals(argName) || (args[i] is IFunction && ((IFunction)args[i]).Name == argName))
                        {
                            args[i] = argFunc;
                        }
                    }
                }

                args.RemoveRange(0, closeBracketInd + 1);
                args.Insert(0, argumentableFunc);
            }
            #endregion

            #region CollapseAllInOneFunction

            var count = args.Count - 1;

            CollapseArray (args, 0, ref count, out error);
            if (error != ErrorSyntax.None)
                return null;
            #endregion

            #region SetNameForFinalFunction
            IFunction finalFunction = null;

            if (args[0] is ArgumentableFunctionsBase)
            {
                ((ArgumentableFunctionsBase)args[0]).CalcFunction = (IFunction)args[2];
                finalFunction = (IFunction)args[0];
            } else
			if (args.Count == 3)
            {
                if (args[1].Equals(Operation.Equal))
                {
                    ((IFunction)args[2]).Rename((args[0] is string) ? (string)args[0] : ((IFunction)args[0]).Name);
                    finalFunction = new NonargumentableFunction((IFunction)args[2]);
					finalFunction.Rename(((IFunction)args[2]).Name);
                }
            } else
            {
                if (args.Count == 1)
                {
                    ((IFunction)args[0]).Rename(null);
                    finalFunction = (IFunction)args[0];
                }
            }
            #endregion

            if (finalFunction == null)
                error = ErrorSyntax.FunctionIsNotCollapsed;

            return finalFunction;
		}
        
		private static int FindCloseBracket(ArrayList data, int openBracketIndex){

			int counter = 0;

			for (int i = openBracketIndex+1; i < data.Count; i++){
			
				if (data[i].Equals(Operation.CloseBracket))
				{
					counter--;
				}
				if (data[i].Equals(Operation.OpenBracket))
				{
					counter++;
				}

				if (counter < 0)
					return i;
			}

            return -1;
		}

        #region CollapseExpression
        private static void CollapseArgumentFunctions(ArrayList List, int startBracketIndex, ref int endBracketIndex, out ErrorSyntax error)
        {
            error = ErrorSyntax.None;
            if (startBracketIndex>= List.Count || startBracketIndex < 0 || !List[startBracketIndex].Equals(Operation.OpenBracket))
            {
                error = ErrorSyntax.NotFoundOpenBracket;
                return;
            }
            if (endBracketIndex >= List.Count || endBracketIndex < 0 || !List[endBracketIndex].Equals(Operation.CloseBracket))
            {
                error = ErrorSyntax.NotFoundCloseBracket;
                return;
            }

            int endIndex = endBracketIndex;

            for (int i = endIndex; i >= startBracketIndex; i--)
            {
                bool endWork = i == startBracketIndex;
                bool separator = List[i].Equals(Operation.ArgsSeparator1) || List[i].Equals(Operation.ArgsSeparator2);

                if (endWork || separator)
                {
                    if (endIndex - i < 2)
                    {
                        error = ErrorSyntax.ArgumentNotFound;
                        return;
                    }

                    var sEI = endIndex;
                    CollapseArray(List, i, ref endIndex, out error);
                    if (error != ErrorSyntax.None)
                        return;
                    endBracketIndex -= sEI - endIndex;
                }
                if (separator)
                {
                    List.RemoveAt(i);
					endBracketIndex--;
                    endIndex = i;
                }
            }
        }

		private static void CollapseArray(ArrayList List, int startIndex, ref int endIndex, out ErrorSyntax error)
        {
			while (CollapseArrayBracketIteration (List, startIndex, ref endIndex, out error)) {}
			CollapseIteration(List, startIndex, ref endIndex, out error);
		}

		private static bool CollapseArrayBracketIteration(ArrayList List, int startIndex, ref int endIndex, out ErrorSyntax error)
        {
            error = ErrorSyntax.None;

			int openBracketIndex = -1;
			for (int i = startIndex; i <= endIndex; i++){
				if (List[i].Equals(Operation.OpenBracket)){
					openBracketIndex = i;
				}

				if (List[i].Equals(Operation.CloseBracket)){

                    if (openBracketIndex < 0)
                        return false;

                    var tmp = i;

					CollapseIteration(List, openBracketIndex, ref i, out error);

                    endIndex -= (tmp - i);

					//var temp = List [openBracketIndex + 1];
					List.RemoveAt (openBracketIndex+2);
					List.RemoveAt (openBracketIndex);
                    endIndex -= 2;
					return true;
				}
			}
			return false;
		}

		private static void CollapseIteration(ArrayList List, int startIndex,ref int endIndex, out ErrorSyntax error)
        {
			while (CollapseOpers(List, startIndex, ref endIndex,out error, Operation.Power)){}
			while (CollapseOpers(List, startIndex, ref endIndex, out error, Operation.Multiply,Operation.DivMod, Operation.Divide)){}
			while (CollapseOpers(List, startIndex, ref endIndex, out error, Operation.Plus, Operation.Minus)){}
		}

		private static bool CollapseOpers(ArrayList List, int startIndex, ref int endIndex, out ErrorSyntax error, params Operation[] oper){
            error = ErrorSyntax.None;

            for (int i = startIndex; i<= endIndex; i++)
			{
				Operation curOper = Operation.None;

				foreach (var o in oper)
				{
					if (List [i].Equals (o)) {
						curOper = o;
						break;
					}
				}

				if (curOper != Operation.None)
				{
                    if (i - 1 < 0)
                    {
                        error = ErrorSyntax.FirstSymbolIsOperation;
                        return false;
                    }

                    if (i + 1 >= List.Count)
                    {
                        error = ErrorSyntax.LastSymbolIsOperation;
                        return false;
                    }

                    if (!(List[i - 1] is IFunction) || !(List[i + 1] is IFunction))
                    {
						Console.WriteLine (List[i - 1] + " " + List[i + 1]);
                        error = ErrorSyntax.ObjectIsNotIFunction;
                        return false;
                    }

					var newOperation = new OperationFunc ((IFunction)List[i-1],(IFunction)List[i+1], curOper);
					List.RemoveRange (i-1, 3);
					List.Insert (i - 1, newOperation);
					endIndex -= 2;
					return true;
				}
			}
			return false;
		}
        #endregion

        #region ErrorsBlock
        private static bool EmptySrc(string src, out ErrorSyntax error)
        {
            if (string.IsNullOrEmpty(src))
            {
                error = ErrorSyntax.SourceIsEmpty;
                return true;
            }
            error = ErrorSyntax.None;
            return false;
        }
        #endregion
    }
}

