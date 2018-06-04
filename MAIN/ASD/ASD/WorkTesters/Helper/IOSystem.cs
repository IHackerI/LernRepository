using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.WorkTesters.Helper
{
    /// <summary>
    /// Инструменты ввода/вывода
    /// Облегчают жизнь, путём обеспечения удобного общения с пользователем
    /// </summary>
    public class IOSystem
    {
        //static int testCounter;

        /// <summary>
        /// Запрашивает пользователя и сразу запускает нужный тест
        /// </summary>
        public static bool InterfacedViewChoice(string[] choiceValues, Delegate[] testerMethods)
        {

            var choiceNum = SimpleChoice("Выберите тип теста :\r\n", choiceValues);

            if (choiceNum < 0)
            {
                return false;
            }
                        
            Console.WriteLine("Начало теста");
            Console.WriteLine();

            testerMethods[choiceNum].Method.Invoke(null, null);

            Console.WriteLine();
            Console.WriteLine("Конец теста");
            

            return true;
        }

        /// <summary>
        /// Выводит выбор теста
        /// </summary>
        public static int SimpleChoice(string header, string[] choiceValues)
        {
            Console.WriteLine(header);

            for (int index = 0; index < choiceValues.Length; index++)
            {
                Console.WriteLine((index + 1) + "   " + choiceValues[index]);
            }

            Console.WriteLine();

            var input = Console.ReadLine();
            Console.WriteLine();
            if (!int.TryParse(input, out int choice))
            {
                return -1;
            }

            if (choice < 1 || choice > choiceValues.Length)
            {
                return -1;
            }

            return choice - 1;
        }

        /// <summary>
        /// Выводит выбор теста с проверкой на правильность данных
        /// </summary>
        public static int SafeSimpleChoice(string header, string[] choiceValues)
        {
            while (true) {
                var choiceNum = SimpleChoice(header, choiceValues);

                if (choiceNum < 0)
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                }

                return choiceNum;
            }
        }

        /// <summary>
        /// Запрашивает у пользователя ввести число
        /// </summary>
        public static int GetInt(string getText)
        {
            int ansInt;
            while (true)
            {
                Console.Write(getText);

                if (!int.TryParse(Console.ReadLine(), out ansInt))
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }

            return ansInt;
        }
    }
}
