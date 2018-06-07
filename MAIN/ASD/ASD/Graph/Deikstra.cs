using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Graph
{
    /// <summary>
    /// Реализация алгоритма Дейкстры. Содержит матрицу смежности в виде массивов вершин и ребер
    /// </summary>
    public class Deikstra<T>
    {
        /// <summary>
        /// Запуск алгоритма расчета
        /// </summary>
        /// <param name="start"></param>
        public static List<Vertex<T>> Calc(Graph<T> graph, Vertex<T> start, Vertex<T> stop)
        {
            ClearGraph(graph);

            OneStep(start);
            foreach (var point in graph.GetVertexesEnumer())
            {
                var anotherP = GetAnotherUncheckedPoint(graph);
                if (anotherP != null)
                {
                    OneStep(anotherP);
                }
                else
                {
                    break;
                }
            }

            var ans = MinPath(start, stop);

            ClearGraph(graph);

            return ans;
        }

        private static void ClearGraph(Graph<T> graph)
        {
            foreach (var point in graph.GetVertexesEnumer())
            {
                point.IsChecked = false;
                point.Distance = int.MaxValue;
                point.PrevVert = null;
            }
        }

        /// <summary>
        /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
        /// </summary>
        /// <param name="iterPoint"></param>
        public static void OneStep(Vertex<T> iterPoint)
        {
            foreach (var nextV in iterPoint.GetNeghboursEnumer())
            {
                if (nextV.IsChecked == false)//не отмечена
                {
                    var newDist = iterPoint.Distance + 1;
                    if (nextV.Distance > newDist)
                    {
                        nextV.Distance = newDist;
                        nextV.PrevVert = iterPoint;
                    }
                }
            }
            iterPoint.IsChecked = true;//вычеркиваем
        }

        /// <summary>
        /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
        /// </summary>
        /// <returns></returns>
        private static Vertex<T> GetAnotherUncheckedPoint(Graph<T> g)
        {
            IEnumerable<Vertex<T>> pointsuncheck = from p in g.GetVertexesEnumer() where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().Distance;
                var minPoint = pointsuncheck.First();
                foreach (var p in pointsuncheck)
                {
                    if (p.Distance < minVal)
                    {
                        minVal = p.Distance;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }

        public static List<Vertex<T>> MinPath(Vertex<T> start, Vertex<T> end)
        {
            var listOfpoints = new List<Vertex<T>>();
            var temp = end;
            while (temp != start && temp != null)
            {
                listOfpoints.Add(temp);
                temp = temp.PrevVert;
            }

            if (temp == start)
                listOfpoints.Add(temp);

            return listOfpoints;
        }
    }
}
