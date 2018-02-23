using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public class Matrix
    {  /// <summary>
       /// Rows.
       /// </summary>
        private int n;

        /// <summary>
        /// Columns.
        /// </summary>
        private int m;

        /// <summary>
        /// Matrix.
        /// </summary>
        private double[,] arr;
        static int count = 0;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Matrix()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Matrix(int n, int m)
        {
            Random r = new Random();
            this.n = n;
            this.m = m;
            arr = new double[n, m];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    arr[i, j] = r.Next(0, 100);
                }
            }
            count++;
        }

        /// <summary>
        /// Constructing matrix with given pair of rows and columns.
        /// </summary>
        /// <param name="size"></param>
        public Matrix(Tuple<int, int> size) : this(size.Item1, size.Item2)
        {
        }

        /// <summary>
        /// Getter and setter.
        /// </summary>
        public Tuple<int, int> Size
        {
            get
            {
                return Tuple.Create(n, m);
            }

            private set
            {
                n = value.Item1;
                m = value.Item2;
            }
        }

        /// <summary>
        /// Determining if the given indexes are out of range or not.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="jcolumn"></param>
        /// <returns></returns>
        private bool IsInRange(int i, int j)
        {
            if (i >= n || j >= m)
            {
                Console.WriteLine("Out of range.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Indexer with given rows and columns.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public double this[int i, int j]
        {

            get
            {
                return arr[i, j];
            }

            private set
            {
                arr[i, j] = value;
            }
        }

        /// <summary>
        /// Overloading operator +.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            Matrix ans = new Matrix();
            if (lhs.Size.Item1 != rhs.Size.Item1 || lhs.Size.Item2 != rhs.Size.Item2)
            {
                Console.WriteLine("Sizes of matrices you want to add must be equal.");
                return ans;
            }
            ans = new Matrix(lhs.Size);
            for (int i = 0; i < ans.Size.Item1; ++i)
            {
                for (int j = 0; j < ans.Size.Item2; ++j)
                {
                    ans[i, j] = lhs[i, j] + rhs[i, j];
                }
            }

            return ans;
        }

        /// <summary>
        /// Scalar multiplication for given matrix.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Matrix ScalarMultiplication(double num)
        {
            Matrix ans = new Matrix(this.Size);
            for (int i = 0; i < ans.Size.Item1; ++i)
            {
                for (int j = 0; j < ans.Size.Item2; ++j)
                {
                    ans[i, j] *= num;
                }
            }

            return ans;
        }

        /// <summary>
        /// Overloading operator *.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            Matrix ans = new Matrix();
            if (lhs.Size.Item2 != rhs.Size.Item1)
            {
                Console.WriteLine("The columns of first matrix must be equal to the rows of second matrix.");
                return ans;
            }
            ans = new Matrix(lhs.Size.Item1, rhs.Size.Item2);

            for (int i = 0; i < lhs.Size.Item1; ++i)
            {
                for (int j = 0; j < rhs.Size.Item2; ++j)
                {
                    ans[i, j] = 0;
                    for (int k = 0; k < lhs.Size.Item2; ++k)
                    {
                        ans[i, j] += lhs[i, k] * rhs[k, j];
                    }
                }
            }
            return ans;
        }

        /// <summary>
        /// Finding largest element of matrix.
        /// </summary>
        /// <returns></returns>
        public double LargestElem()
        {
            double largestElem = this[0, 0];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (arr[i, j] >= largestElem)
                    {
                        largestElem = arr[i, j];
                    }
                }
            }
            return largestElem;
        }

        /// <summary>
        /// Finding smallest element of matrix.
        /// </summary>
        /// <returns></returns>
        public double SmallestElem()
        {
            double smallestElem = this[0, 0];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (arr[i, j] <= smallestElem)
                    {
                        smallestElem = arr[i, j];
                    }
                }
            }
            return smallestElem;
        }

        /// <summary>
        /// Determining if the given matrix is orthogonal or not.
        /// </summary>
        /// <returns></returns>
        public bool IsOrthogonal()
        {
            //case of wrong input
            if (n != m)
            {
                Console.WriteLine("Matrix must be square.");
                return false;
            }

            Matrix T = this.Transpose();
            Matrix Id = new Matrix(n, n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == j)
                        Id[i, j] = 1;
                    else
                        Id[i, j] = 0;
                }
            }

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if ((this * T)[i, j] != Id[i, j])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Transposing given matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix ans = new Matrix(this.Size.Item2, this.Size.Item1);
            for (int i = 0; i < ans.Size.Item1; ++i)
            {
                for (int j = 0; j < ans.Size.Item2; ++j)
                {
                    ans[i, j] = this[j, i];
                }
            }
            return ans;
        }

        /// <summary>
        /// Output.
        /// </summary>
        public void Read()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    Console.Write(Math.Round(this[i, j], 4) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Translating given point with point p.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Matrix Translation(Matrix point)
        {
            if (this.n != 1 || point.n != 1 || this.m != point.m)
            {
                Console.WriteLine("Enter points 1xn");
                return new Matrix();
            }
            Matrix matrix = new Matrix(m + 1, m + 1);

            int k = 0;

            for (int i = 0; i < m + 1; ++i)
            {
                for (int j = 0; j < m + 1; ++j)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                    else if (j != m)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        matrix[i, j] = this[0, k++];
                    }
                }
            }

            Matrix pointT = new Matrix(m + 1, 1);
            for (int i = 0; i < m; ++i)
            {
                pointT[i, 0] = point[0, i];
            }

            pointT[m, 0] = 1;
            matrix *= pointT;

            return matrix;
        }

        /// <summary>
        /// Scaling with given vector of factors.
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        public Matrix Scaling(Matrix v)
        {
            if (this.n != 1 || v.n != 1 || this.m != v.m)
            {
                Console.WriteLine("Enter vectors 1xn");
                return new Matrix();
            }

            Matrix matrix = new Matrix(m, m);
            int k = 0;
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (i == j)
                    {
                        matrix[i, j] = v[0, k++];
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }

                }
            }

            Matrix v2 = new Matrix(n, m);
            v2 = this;
            v2 = v2.Transpose();

            matrix = matrix * v2;
            return matrix;
        }


        /// <summary>
        /// 2D Rotation with given angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>

        public Matrix Rotation(double angle)
        {
            if (this.n == 1 && this.m == 2)
            {
                Matrix R = new Matrix(2, 2);
                R[0, 0] = Math.Cos(angle);
                R[0, 1] = (-1) * Math.Sin(angle);
                R[1, 0] = Math.Sin(angle);
                R[1, 1] = Math.Cos(angle);
                return R * (this.Transpose());
            }
            return new Matrix();
        }

        /// <summary>
        /// 3D Rotation with given angle about the given axis.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public Matrix Rotation(double angle, string axis)
        {
            if (this.n == 1 && this.m == 3)
            {
                Matrix R = new Matrix(3, 3);

                //Rotation Matrix if the axis is x.
                if (axis == "x")
                {
                    R[0, 0] = 1;
                    R[0, 1] = 0;
                    R[0, 2] = 0;
                    R[1, 0] = 0;
                    R[1, 1] = Math.Cos(angle);
                    R[1, 2] = (-1) * Math.Sin(angle);
                    R[2, 0] = 0;
                    R[2, 1] = Math.Sin(angle);
                    R[2, 2] = Math.Cos(angle);
                }

                //Rotation Matrix if the axis is y.
                if (axis == "y")
                {
                    R[0, 0] = Math.Cos(angle);
                    R[0, 1] = 0;
                    R[0, 2] = Math.Sin(angle);
                    R[1, 0] = 0;
                    R[1, 1] = 1;
                    R[1, 2] = 0;
                    R[2, 0] = (-1) * Math.Sin(angle);
                    R[2, 1] = 0;
                    R[2, 2] = Math.Cos(angle);
                }

                //Rotation Matrix if the axis is z.
                if (axis == "z")
                {
                    R[0, 0] = Math.Cos(angle);
                    R[0, 1] = (-1) * Math.Sin(angle);
                    R[0, 2] = 0;
                    R[1, 0] = Math.Sin(angle);
                    R[1, 1] = Math.Cos(angle);
                    R[1, 2] = 0;
                    R[2, 0] = 0;
                    R[2, 1] = 0;
                    R[2, 2] = 1;
                }
                return R * (this.Transpose());
            }

            //Wrong output case.
            Console.WriteLine("Enter correct vector size or correct axis name.");
            return new Matrix();
        }


        /// <summary>
        /// Basic matrix transformations.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="angle"></param>
        /// <param name="factors"></param>
        /// <returns></returns>
        public Matrix Transformation(Matrix point, double angle, Matrix factors)
        {
            Matrix ans = new Matrix(n, m);
            ans.Translation(point);
            ans.Rotation(angle);
            ans.Scaling(factors);
            return ans;
        }

        /// <summary>
        /// Overloaded matrix transformation in 3d
        /// </summary>
        /// <param name="point"></param>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <param name="factors"></param>
        /// <returns></returns>
        public Matrix Transformation(Matrix point, double angle,string axis, Matrix factors)
        {
            Matrix ans = new Matrix(n, m);
            ans.Translation(point);
            ans.Rotation(angle, axis);
            ans.Scaling(factors);
            return ans;
        }

        /// <summary>
        /// Function to determine the sign of each element.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private int SignOfElem(int row, int column)
        {
            if ((row + column) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Creating matrix of size (n-1)x(n-1) from nxn matrix.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        static Matrix CreateSmallerMatrix(Matrix matrix, int row, int col)
        {
            int size = matrix.n;
            Matrix ans = new Matrix(size - 1, size - 1);
            int x = 0;
            int y = 0;
            for (int i = 0; i < size; i++, x++)
            {
                if (i != row)
                {
                    y = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (j != col)
                        {
                            ans[x, y] = matrix[i, j];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return ans;
        }

        /// <summary>
        /// Function for counting determinant of given matrix.
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        public double Determinant(Matrix A)
        {
            if (A.n != A.m)
            {
                return 0;
            }

            int size = A.n;
            if (size == 1)
            {
                return (A[0, 0]);
            }

            if (size == 2)
            {
                return ((A[0, 0] * A[1, 1]) - (A[1, 0] * A[0, 1]));
            }
            double ans = 0;
            for (int j = 0; j < size; j++)
            {
                //getting det recursively 
                Matrix smaller = CreateSmallerMatrix(A, 0, j);
                ans = ans + A[0, j] * (Determinant(smaller) * SignOfElem(0, j));
            }
            return ans;
        }

        /// <summary>
        /// Finding inverse matrix of given square matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix InverseMatrix()
        {
            if (this.n != this.m)
            {
                Console.WriteLine("Inverse matrix exists only for square Matrices.");
                return new Matrix();
            }

            Matrix matrix = this;

            if (Determinant(matrix) == 0)
            {
                Console.WriteLine("As the determinant of this matrix is null inverse matrix doesn't exist.");
                return new Matrix();
            }

            Matrix ans = new Matrix(n, n);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == j)
                        ans[i, j] = 1;
                    else
                        ans[i, j] = 0;
                }
            }

            // getting upper triangular matrix
            for (int i = 0; i < n; ++i)
            {
                int curRow = i;
                //pivoting, finding biggest element in the column

                double maxEl = 0;
                int maxRow = curRow;
                for (int j = curRow; j < n; ++j)
                {
                    double temp = Math.Abs(matrix[j, curRow]);
                    if (temp > maxEl)
                    {
                        maxEl = temp;
                        maxRow = j;
                    }
                }

                if (maxRow != curRow)
                {
                    //swapping 
                    for (int j = 0; j < n; ++j)
                    {
                        double temp = ans[curRow, j];
                        ans[curRow, j] = ans[maxRow, j];
                        ans[maxRow, j] = temp;
                    }
                    for (int j = curRow; j < n; ++j)
                    {
                        double temp = matrix[curRow, j];
                        matrix[curRow, j] = matrix[maxRow, j];
                        matrix[maxRow, j] = temp;
                    }
                }

                for (int j = curRow + 1; j < n; ++j)
                {

                    double temp = matrix[j, curRow] / matrix[curRow, curRow];

                    if (temp != 0)
                    {
                        for (int k = curRow; k < n; ++k)
                        {
                            matrix[j, k] -= temp * matrix[curRow, k];
                        }
                        for (int k = 0; k < n; ++k)
                        {
                            ans[j, k] -= temp * ans[curRow, k];
                        }
                    }
                }
            }

            //getting diagonal matrix
            for (int i = n - 1; i >= 0; --i)
            {
                int curRow = i;
                for (int j = curRow - 1; j >= 0; --j)
                {
                    double temp = matrix[j, curRow] / matrix[curRow, curRow];
                    matrix[j, curRow] = 0;

                    if (temp == 0)
                    {
                        continue;
                    }

                    else
                    {
                        for (int k = 0; k < n; ++k)
                        {
                            ans[j, k] = ans[j, k] - temp * ans[curRow, k];
                        }
                    }
                }
            }

            //making 1s on diagonal
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ans[i, j] = ans[i, j] / matrix[i, i];
                }
            }

            return ans;
        }
    }
}


