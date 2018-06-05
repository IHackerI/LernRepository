using System;

namespace ChislMethods.LinAl
{
    public class Matrix
    {
        public double[,] Args { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Boolean IsSquare => this.Row == this.Col;

        public Matrix(int dimension, double value)
        {
            this.Row = this.Col = dimension;
            this.Args = new double[Row, Col];

            for (var index = 0; index < this.Row; ++index)
                this[index, index] = value;
        }

        public Matrix(int row, int col)
        {
            Row = row;
            Col = col;
            Args = new double[Row, Col];
        }

        public Matrix(double[] x)
        {
            Row = x.Length;
            Col = 1;
            Args = new double[Row, Col];
            for (int i = 0; i < Args.GetLength(0); i++)
                for (int j = 0; j < Args.GetLength(1); j++)
                    Args[i, j] = x[i];
        }

        public Matrix(double[,] x)
        {
            Row = x.GetLength(0);
            Col = x.GetLength(1);
            Args = new double[Row, Col];
            for (int i = 0; i < Args.GetLength(0); i++)
                for (int j = 0; j < Args.GetLength(1); j++)
                    Args[i, j] = x[i, j];
        }

        public Matrix(Matrix other)
        {
            this.Row = other.Row;
            this.Col = other.Col;
            Args = new double[Row, Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    this.Args[i, j] = other.Args[i, j];
        }

        public Vector Column(int k)
        {
            var vector = new Vector(this.Row);
            for (int index = 0; index < this.Row; index++) vector[index] = this[index, k];
            return vector;
        }

        public void Column(int k, Vector vector)
        {
            for (int index = 0; index < this.Row; index++) vector[index] = this[index, k];
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
                    return Args[i, j];
            }
            set
            {
                if (i < 0 && j < 0 && i >= Row && j >= Col)
                {
                    Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    Args[i, j] = value;
            }
        }

        public override string ToString()
        {
            string s = string.Empty;
            for (int i = 0; i < Args.GetLength(0); i++)
            {
                for (int j = 0; j < Args.GetLength(1); j++)
                {
                    s += string.Format("{0} ", Args[i, j]);
                }
                s += "\n";
            }
            return s;
        }

        public static Matrix Transposition(Matrix source) // Транспонирование матрицы
        {
            double[,] t = new double[source.Col, source.Row];
            for (int i = 0; i < source.Row; i++)
                for (int j = 0; j < source.Col; j++)
                    t[j, i] = source[i, j];
            return new Matrix(t);
        }

        public static Matrix operator *(Matrix m, double k) // Умножение вектора на число
        {
            Matrix ans = new Matrix(m);
            for (int i = 0; i < ans.Row; i++)
                for (int j = 0; j < ans.Col; j++)
                    ans.Args[i, j] = m.Args[i, j] * k;
            return ans;
        }

        public static Matrix operator *(Matrix m1, Matrix m2) // Умножение матрицы на матрицу
        {
            if (m1.Col != m2.Row) throw new ArgumentException("Multiplication of these two matrices can't be done!");
            double[,] ans = new double[m1.Row, m2.Col];
            for (int i = 0; i < m1.Row; i++)
            {
                for (int j = 0; j < m2.Col; j++)
                {
                    for (int k = 0; k < m2.Row; k++)
                    {
                        ans[i, j] += m1.Args[i, k] * m2.Args[k, j];
                    }
                }
            }
            return new Matrix(ans);
        }

        public static Vector operator *(Matrix m1, Vector v) // Умножение матрицу на вектор
        {
            if (m1.Col != v.GetSize())
            {
                Vector nan = new Vector(0);
                return nan;
            }
            Vector rez = new Vector(m1.Row);

            for (int i = 0; i < m1.Row; i++)
                for (int j = 0; j < m1.Col; j++)
                    rez[i] += m1[i, j] * v[j];

            return rez;
        }


        public static double Determ(Matrix m) // Нахождение определителя матрицы
        {
            if (m.Row != m.Col) throw new ArgumentException("Matrix should be square!");
            double det = 0;
            int length = m.Row;

            if (length == 1) det = m.Args[0, 0];
            if (length == 2) det = m.Args[0, 0] * m.Args[1, 1] - m.Args[0, 1] * m.Args[1, 0];

            if (length > 2)
                for (int i = 0; i < m.Col; i++)
                    det += Math.Pow(-1, 0 + i) * m.Args[0, i] * Determ(m.GetMinor(0, i));

            return det;
        }

        private Matrix GetMinor(int row, int column) // Получение минора матрицы по строке и столбцу
        {
            if (Row != Col) throw new ArgumentException("Матрица должна быть квадратной!");
            double[,] minor = new double[Row - 1, Col - 1];
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Col; j++)
                {
                    if ((i != row) || (j != column))
                    {
                        if (i > row && j < column) minor[i - 1, j] = this.Args[i, j];
                        if (i < row && j > column) minor[i, j - 1] = this.Args[i, j];
                        if (i > row && j > column) minor[i - 1, j - 1] = this.Args[i, j];
                        if (i < row && j < column) minor[i, j] = this.Args[i, j];
                    }
                }
            }
            return new Matrix(minor);
        }

        public Matrix SignedMinor() // Возвращает массив алгебраических дополнений
        {
            double[,] ans = new double[Row, Col];

            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    ans[i, j] = Math.Pow(-1, i + j) * Determ(this.GetMinor(i, j));

            return new Matrix(ans);
        }

        public Matrix InverseMatrix()
        {
            if (Math.Abs(Determ(this)) <= 0.000000001) throw new ArgumentException("Inverse matrix does not exist!");

            double k = 1 / Determ(this);

            Matrix minorMatrix = this.SignedMinor();

            return minorMatrix * k;
        }
        
        public Vector GetRow(int x) // Получение строки в виде вектора
        {
            if (x >= 0 && x < Row)
            {
                Vector row = new Vector(Col);
                for (int j = 0; j < Col; j++)
                    row.SetElement(Args[x, j], j);
                return row;
            }
            Vector nan = new Vector(0);
            return nan;
        }

        public double Norma()
        {
            var result = 0.0;

            this.Foreach((r, c, m) => result += m[r, c] * m[r, c]);

            return Math.Sqrt(result);
        }

        public Matrix Foreach(Action<int, int, Matrix> action)
        {
            for (int row = 0; row < this.Row; ++row)
                for (var column = 0; column < this.Col; ++column)
                    action(row, column, this);

            return this;
        }

        public void SetRow(Vector vr, int r)
        {
            if ((Col != vr.GetSize()) || r < 0 || r >= Row) return;
            for (int j = 0; j < Col; j++)
                Args[r, j] = vr.GetElement(j);
        }

        public void View() // Вывод матрицы
        {
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Col; j++)
                    Console.Write("{0} ", this[i, j]);
                Console.WriteLine(" ");
            }
        }

        public void SwapRows(int index1, int index2)
        {
            for (var column = 0; column < this.Col; ++column)
            {
                var tmp = this[index1, column];
                this[index1, column] = this[index2, column];
                this[index2, column] = tmp;
            }
        }

        public Matrix SetColumn(Vector vector, int k)
        {
            for (int i = 0; i < vector.size; i++)
            {
                this[i, k] = vector[i];
            }
            //this.Foreach((int c, ref double v) => v = vector[c], DimensionType.Row, k);
            return this;
        }
    }
}
