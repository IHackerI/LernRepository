 using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    /*
    public class MatrixO
    {
        public double[,] matrix;
        public int Row = 0, Col = 0; //начальное кол-во строк и столбцов      
       
        public MatrixO(int row, int col)
        {
            // конструктор матрицы
            matrix = new double[row, col];
            Row = row;
            Col = col;
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 && j < 0 && i >= Row && j >= Col)
                {
                    Console.WriteLine(" Индексы вышли за пределы матрицы ");
                    return 0;
                }
                else
                    return matrix[i, j];
            }
            set
            {
                if (i < 0 && j < 0 && i >= Row && j >= Col)
                {
                    Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    matrix[i, j] = value;
            }
        }

        public MatrixO(double[,] mm) // конструктор
        {
            this.Row = mm.GetLength(0);
            this.Col = mm.GetLength(1);
            matrix = new double[Row, Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    matrix[i, j] = mm[i, j];
        }

        public MatrixO(MatrixO mm) // конструктор
        {
            this.Row = mm.Row;
            this.Col = mm.Col;
            matrix = new double[Row, Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    matrix[i, j] = mm[i, j];
        }
        
        internal MatrixO Gauss(MatrixO mtx)
        {
            var size = mtx.Row;//mtx.si

            //Vector v = new Vector(vector);

            //Vector nan = new Vector(0);

            //Vector b = new Vector(vector);
            //double[] x = new double[size];
            double max;
            int jmax;
            for (int k = 0; k < size; k++)
            {
                // поиск макс по модулю элемента в к столбце начиная с к элемента 
                max = Math.Abs(mtx[k, k]);
                jmax = k;
                for (int j = k + 1; j < size; j++)
                {
                    if (Math.Abs(mtx[j, k]) > max)
                    {
                        max = Math.Abs(mtx[j, k]);
                        jmax = j;
                    }
                }
                if (jmax != k)
                {
                    //обмен строк 
                    for (int i = 0; i < size; i++)
                    {
                        double temp = mtx[k, i];
                        mtx[k, i] = mtx[jmax, i];
                        mtx[jmax, i] = temp;
                        //double temp1 = b[k];
                        //b[k] = b[jmax];
                        //b[jmax] = temp1;
                    }
                }
                //Приведение к треугольному виду 
                double m;
                for (int z = 1; z < size; z++)
                {
                    for (int j = z; j < size; j++)
                    {
                        m = mtx[j, z - 1] / mtx[z - 1, z - 1];
                        for (int i = 0; i < size; i++)
                            mtx[j, i] = mtx[j, i] - m * mtx[z - 1, i];
                        //b[j] = b[j] - m * b[z - 1];
                    }
                }
                //проверка на особбенность матрицы 
                if (max == 0) return null;
            }
            //обратный ход 
            //for (int q = size - 1; q >= 0; q--)
            //{
            //    for (int j = q + 1; j < size; j++)
            //        b[q] -= mtx[q, j] * b[j];
            //    b[q] = b[q] / mtx[q, q];
            //}
            return mtx;

            //Console.WriteLine("Метод гаусса не выполнен!");
            //return null;
            //throw new NotImplementedException();
        }

        public void View()
        {
           for (int i = 0; i < this.Row; i++)
           {
               for (int j = 0; j < this.Col; j++)
                   Console.Write("{0} ", this[i, j]);
               Console.WriteLine(" ");
           }
            
        }

        public MatrixO addition(MatrixO b)
        {
            if (Row == b.Row && Col == b.Col)
            {
                MatrixO c = new MatrixO(Row, Col);

                for (int i = 0; i < c.Row; i++)
                    for (int j = 0; j < c.Col; j++)
                        c[i, j] = matrix[i, j] + b[i, j];

                return c;
            }


            MatrixO nan = new MatrixO(0, 0);
            Console.Write("Матрицы нельзя сложить.");
            return nan;
            
        }

        public MatrixO substraction(MatrixO b)
        {
            if (Row == b.Row && Col == b.Col)
            {
                MatrixO c = new MatrixO(Row, Col);

                for (int i = 0; i < c.Row; i++)
                    for (int j = 0; j < c.Col; j++)
                        c[i, j] = matrix[i, j] - b[i, j];

                return c;
            }
            MatrixO nan = new MatrixO(0, 0);
            Console.Write("Матрицы нельзя вычесть.");
            return nan;
            
        }

        public MatrixO multiplication(double x)
        {
            MatrixO c = new MatrixO(Row, Col);

            for (int i = 0; i < c.Row; i++)
                for (int j = 0; j < c.Col; j++)
                    c[i, j] = matrix[i, j] * x;

            return c;
        }

        public MatrixO multiplication(MatrixO b)
        {
            if (Col == b.Row)
            {
                MatrixO c = new MatrixO(Row, b.Col);


                for (int i = 0; i < Row; i++)
                    for (int j = 0; j < b.Col; j++)
                        for (int k = 0; k < b.Row; k++)
                            c[i, j] += matrix[i, k] * b[k, j];
                return c;
            }
            MatrixO nan = new MatrixO(0, 0);
            Console.Write("Матрицы нельзя умножить.");
            return nan;
            
        }

        public Vector MultiplyVector(Vector c) // умножение матрицы на вектор
        {
            if (Col != c.GetSize())
            {   Vector nan = new Vector(0);
                return nan;
            }
            Vector rez = new Vector(Row);

            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    rez[i] += matrix[i, j] * c[j];
               
            return rez;
        }

        public MatrixO transp(MatrixO r)
        {
            MatrixO c = new MatrixO(r.Col, r.Row);

            for (int i = 0; i < c.Row; i++)
                for (int j = 0; j < c.Col; j++)
                    c[i, j] = r[j, i];
            return c;
        }

        public MatrixO identityMatrix(int m) // единичная
        {
            MatrixO c = new MatrixO(m, m);

            for (int i = 0; i < c.Row; i++)
                for (int j = 0; j < c.Col; j++)
                {
                    if (i == j)
                        c[i, j] = 1;
                    else
                        c[i, j] = 0;
                }
            return c;
        }

        public Vector getColumn(int x) // получение столбца в виде вектора
        {            
            if (x >= 0 && x < Col)
            {
                Vector column = new Vector(Row);
                for (int j = 0; j < Row; j++)
                    column.SetElement(matrix[j, x], j);
                return column;
            }
            Vector nan = new Vector(0);
            return nan;
        }

        public Vector getRow(int x) // получение строки в виде вектора
        {
            if (x >= 0 && x < Row)
            {
                Vector row = new Vector(Col);
                for (int j = 0; j < Col; j++)
                    row.SetElement(matrix[x, j], j);
                return row;
            }
            Vector nan = new Vector(0);
            return nan;
        }

        public void SetRow(Vector vr, int r)
        {     
            if (( Col != vr.GetSize() ) || r < 0 || r >= Row) return;
            for (int j = 0; j < Col; j++)
                matrix[r, j] = vr.GetElement(j);
        }

        public void SetCol(Vector vc, int c)
        {            
            if (( Row != vc.GetSize() ) || c < 0 || c >= Col) return;
            for (int j = 0; j < Row; j++)
                matrix[c, j] = vc.GetElement(j);
        }

        public double deter(MatrixO r) // определитель матрицы
        {
            double det = 0;
            if (r.Row != r.Col)
            {
                Console.WriteLine("Матрица не квадратная");
                return double.NaN;
            }

            if (r.Row == 1)
                det = r[0, 0];

            else if (r.Row == 2)
                det = r[0, 0] * r[1, 1] - r[0, 1] * r[1, 0];

            else if (r.Row > 2)
            {
                for (int i = 0; i < r.Col; i++)
                {
                    MatrixO c = new MatrixO(r.Row - 1, r.Col - 1);
                    det += Math.Pow(-1, i + 2) * r[0, i] * deter(minor(r, 0, i));
                }
            }
            return det;
        }

        public MatrixO minor(MatrixO r, int m, int n) // минор матрицы
        {
            MatrixO c = new MatrixO(r.Row - 1, r.Col - 1);

            for (int i = 0, q = 0; q < c.Row; i++, q++)
                for (int j = 0, p = 0; p < c.Col; j++, p++)
                {
                    if (i == m) i++;
                    if (j == n) j++;
                    c[q, p] = r[i, j];
                }
            return c;
        }

        public MatrixO cofactor(MatrixO r) // алгебраическое дополнение
        {
            MatrixO c = new MatrixO(r.Row, r.Col);
            for (int i = 1; i <= r.Row; i++)
            {
                for (int j = 1; j <= r.Col; j++)
                {
                    c[i - 1, j - 1] = Math.Pow(-1, i + j) * deter(minor(r, i - 1, j - 1));
                }
            }
            return c;
        }

        public MatrixO inverse(MatrixO r) //обратная матрица
        {
            if (deter(r) == 0)
            {
                MatrixO nan = new MatrixO(0, 0); 
                Console.Write("У матрицы нет обратной.");
                return nan;
            }
            MatrixO c;
            c = transp(cofactor(r).multiplication(( 1 / deter(r) )));

            return c;
        } 

        public int getR() { return Row; }

        public int getC() { return Col; }

        public void SwapRow(int a, int b)// перестановка строк
        {            
            if (a < 0 || b < 0 || a > Row || b > Row || ( a == b )) return;
            
            Vector v1 = getRow(a);
            Vector v2 = getRow(b);
            
            SetRow(v1, b);
            SetRow(v2, a);
        }

        public void SwapColumn(int a, int b) // перестановка столбцов
        {
            if (a < 0 || b < 0 || a > Col || b > Col || ( a == b ))
                return;

            Vector v1 = getColumn(a);
            Vector v2 = getColumn(b);
            SetCol(v1, b);
            SetCol(v2, a);
        }

        public MatrixO Copy() // копирование матрицы
        {

            MatrixO mat = new MatrixO(Row, Col);
            for (int i = 0; i < Col; i++)
                for (int j = 0; j < Row; j++)
                    mat[i, j] = matrix[i, j];
            return mat;

        }
    }
*/
}
