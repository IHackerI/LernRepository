using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Helpers
{
    public class ChoiceController
    {
        static int testCounter;

        public static bool ViewChoice(string[] choiceValues, Delegate[] testerMethods)
        {
            testCounter++;

            Console.WriteLine("Выберите тип тестирования " + testCounter + ":\r\n");

            for (int index = 0; index < choiceValues.Length; index++)
            {
                Console.WriteLine((index+1) + "   " + choiceValues[index]);
            }

            Console.WriteLine();

            var input = Console.ReadLine();
            Console.WriteLine();
            if (!int.TryParse(input, out int choice))
            {
                testCounter--;
                return false;
            }

            if (choice < 1 || choice > choiceValues.Length)
            {
                testCounter--;
                return false;
            }
            
            Console.WriteLine("-------------Начало тестирования " + testCounter + "-------------");
            Console.WriteLine();

            testerMethods[choice-1].Method.Invoke(null, null);

            Console.WriteLine();
            Console.WriteLine("-------------Конец тестирования " + testCounter + "-------------");

            testCounter--;

            return true;
        }
    }
}
