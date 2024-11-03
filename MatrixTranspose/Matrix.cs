using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTranspose
{
    public class Matrix : IMatrix
    {
        private int _n;
        private int _m;
        private List<Tuple<int, int, double>> _nonZeroElements = new List<Tuple<int, int, double>>();

        public int N { get => _n; set => _n = value <= 0 ? throw new ArgumentOutOfRangeException("Amount of rows can't be < 0") : value; }
        public int M { get => _m; set => _m = value <= 0 ? throw new ArgumentOutOfRangeException("Amount of columns can't be < 0") : value; }
        public List<Tuple<int, int, double>> NonZeroElements
        {
            get => _nonZeroElements;
            set
            {
                if (value == null) throw new ArgumentNullException("List of NonZeroElements can't be empty.");
                foreach (var tuple in value)
                {
                    if (tuple.Item1 < 0 || tuple.Item1 >= N || tuple.Item2 < 0 || tuple.Item2 >= M)
                    {
                        throw new ArgumentOutOfRangeException("Element can't be out of matrix size.");
                    }
                }
                _nonZeroElements = value;
            }
        }

        public Matrix()
        {
            _n = 1;
            _m = 1;
            _nonZeroElements = new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 0, 1.0) };
        }

        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            _nonZeroElements = new List<Tuple<int, int, double>> { };
        }

        public Matrix(int n, int m, List<Tuple<int, int, double>> nonZeroElements)
        {
            N = n;
            M = m;
            NonZeroElements = nonZeroElements;
        }

        public IMatrix Add(IMatrix matrix)
        {
            if (matrix.N != this.N || matrix.M != this.M)
            {
                throw new InvalidOperationException("Matrices must have the same dimensions to be added.");
            }

            var resultNonZeroElements = new List<Tuple<int, int, double>>();
            var tempMatrix = new double[N, M];

            foreach (var elem in NonZeroElements)
            {
                tempMatrix[elem.Item1, elem.Item2] = elem.Item3;
            }

            foreach (var elem in ((Matrix)matrix).NonZeroElements)
            {
                tempMatrix[elem.Item1, elem.Item2] += elem.Item3;
            }

            for (int n = 0; n < N; n++)
            {
                for (int m = 0; m < M; m++)
                {
                    if (tempMatrix[n, m] != 0)
                    {
                        resultNonZeroElements.Add(new Tuple<int, int, double>(n, m, tempMatrix[n, m]));
                    }
                }
            }

            return new Matrix(N, M, resultNonZeroElements);
        }

        public IMatrix Multyply(IMatrix matrix)
        {
            if (this.M != matrix.N)
            {
                throw new InvalidOperationException("Number of columns in the first matrix must match number of rows in the second matrix.");
            }

            var resultNonZeroElements = new List<Tuple<int, int, double>>();
            double[,] tempMatrix = new double[this.N, matrix.M];

            foreach (var elementA in this.NonZeroElements)
            {
                for (int j = 0; j < matrix.M; j++)
                {
                    var elementB = ((Matrix)matrix).NonZeroElements.FirstOrDefault(e => e.Item1 == elementA.Item2 && e.Item2 == j);
                    if (elementB != null)
                    {
                        tempMatrix[elementA.Item1, j] += elementA.Item3 * elementB.Item3;
                    }
                }
            }

            for (int n = 0; n < this.N; n++)
            {
                for (int m = 0; m < matrix.M; m++)
                {
                    if (tempMatrix[n, m] != 0)
                    {
                        resultNonZeroElements.Add(new Tuple<int, int, double>(n, m, tempMatrix[n, m]));
                    }
                }
            }

            return new Matrix(this.N, matrix.M, resultNonZeroElements);
        }

        public IMatrix MultyplyByNumber(double num)
        {
            var resultNonZeroElements = new List<Tuple<int, int, double>>();

            foreach (var element in NonZeroElements)
            {
                resultNonZeroElements.Add(new Tuple<int, int, double>(element.Item1, element.Item2, element.Item3 * num));
            }
            return new Matrix(this.N, this.M, resultNonZeroElements);
        }

        public IMatrix Transpose()
        {
            var transposedNonZeroElements = new List<Tuple<int, int, double>>();

            foreach (var element in NonZeroElements)
            {
                transposedNonZeroElements.Add(new Tuple<int, int, double>(element.Item2, element.Item1, element.Item3));
            }
            return new Matrix(this.M, this.N, transposedNonZeroElements);
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix matrix &&
            N == matrix.N &&
            M == matrix.M &&
            NonZeroElements.Count == matrix.NonZeroElements.Count &&
            NonZeroElements.All(tuple =>
                matrix.NonZeroElements.Any(other =>
                     tuple.Item1 == other.Item1 &&
                     tuple.Item2 == other.Item2 &&
                     tuple.Item3.Equals(other.Item3)));
        }

        public bool NotEquals(Matrix matrix)
        {
            return !Equals(matrix);
        }

        public override int GetHashCode()
        {
            int hashCode = 1495675192;
            hashCode = hashCode * -1521134295 + N.GetHashCode();
            hashCode = hashCode * -1521134295 + M.GetHashCode();

            foreach (var tuple in NonZeroElements)
            {
                hashCode = hashCode * -1521134295 + tuple.Item1.GetHashCode();
                hashCode = hashCode * -1521134295 + tuple.Item2.GetHashCode();
                hashCode = hashCode * -1521134295 + tuple.Item3.GetHashCode();
            }

            return hashCode;
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            double[,] matrix = new double[N, M];

            foreach (var tuple in NonZeroElements)
            {
                matrix[tuple.Item1, tuple.Item2] = tuple.Item3; 
            }

            for (int n = 0; n < N; n++)
            {
                for (int m = 0; m < M; m++)
                {
                    res.Append(matrix[n, m].ToString("G") + " "); 
                }
                res.Append("\n");
            }

            return res.ToString();
        }
    }
}
