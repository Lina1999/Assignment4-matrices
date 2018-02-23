using System;
using Matrices;

class Program
{
    /// <summary>
    /// Main function.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        ///creating matrices
        Matrix matrix1 = new Matrix(4, 5);
        Matrix matrix2 = new Matrix(4, 5);

        //reading 
        matrix1.Read();
        matrix2.Read();
        
        ///adding them
        (matrix1 + matrix2).Read();
       
        ///transposing
        (matrix2.Transpose()).Read();

        ///scalar multiplication
        matrix1.ScalarMultiplication(4).Read();
        Matrix matrix3 = new Matrix(5, 4);
        matrix3.Read();
        Console.WriteLine("Smallest element in this matrix is: " + matrix3.SmallestElem());
        Console.WriteLine("Largest element in this matrix is: " + matrix3.LargestElem());

        ///multiplying matrices
        (matrix1 *matrix3).Read();
        Matrix matrix4 = (matrix1 * matrix3);

        ///finding inverse
        (matrix4.InverseMatrix()).Read();

        ///applying matrix transformations
        Matrix matrix5 = new Matrix(1, 3);
        Matrix matrix6 = new Matrix(1, 3);
        matrix5.Read();
       
        (matrix5.Transformation(matrix6, Math.Asin(1),"y", matrix6)).Read();

    }
}

