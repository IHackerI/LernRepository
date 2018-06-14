using ASD.Sort;
using ChislMethods.WorkTesters.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.WorkTesters
{
    public static class AddonsTester
    {
        public static void TEST()
        {
            #region Создание сортируемого массива
            Console.WriteLine("Рекомендуемые значения: 1000000 массив, 5 потоков ");
            int _count = IOSystem.GetInt("Введите размер генерируемого массива: ");
            int[] setInt = new int[_count];
            int[] setCopy = new int[_count];
            Random rand = new Random();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                setInt[i] = rand.Next(0, 50);
                setCopy[i] = setInt[i];
            }
            #endregion
            
            Console.WriteLine("__________Параллельная сортировка__________ ");
            if (setInt.Length < 1001)
            {
                Console.Write("Несортированное множество: ");
                ShowSet(setInt);
            }
            Stopwatch sw = new Stopwatch();
            var threadsCount = IOSystem.GetInt("Введите кол-во потоков: ");
            GC.TryStartNoGCRegion(9999999);
            sw.Start();
            Sort<int>.ParallelMergeSort(setInt, 0, setInt.Length - 1, threadsCount);
            sw.Stop();
            Console.WriteLine("\n\nВремя: " + sw.ElapsedTicks);
            if (setInt.Length < 1001)
            {
                Console.Write("Cортированное множество: ");
                ShowSet(setInt);
            }
            Console.WriteLine("__________Конец параллельной сортировки__________ ");

            Console.WriteLine("__________Непараллельная сотировка__________ ");
            if (setInt.Length < 1001)
            {
                Console.Write("Несортированное множество: ");
                ShowSet(setCopy);
            }
            sw.Reset();
            sw.Start();
            GC.Collect();
            //GC.EndNoGCRegion();
            Sort<int>.MergeSort(setCopy, 0, setCopy.Length - 1);
            sw.Stop();
            Console.WriteLine("\n\nВремя: " + sw.ElapsedTicks);
            if (setInt.Length < 1001)
            {
                Console.Write("Cортированное множество: ");
                ShowSet(setCopy);
            }
            Console.WriteLine("__________Конец непараллельной сортировки__________ ");

            Console.WriteLine();
        }


        /// <summary>
        /// Отрисовывает переданный массив
        /// </summary>
        static void ShowSet<T>(IList<T> arr)
        {
            var enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current + " ");
            }
            Console.WriteLine();
        }
    }
}
