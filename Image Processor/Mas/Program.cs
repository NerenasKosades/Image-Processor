using System;

namespace Mas
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] mas1 = new int[3, 3] { { 1, 2, 3 } ,{ 4, 5, 6 } };
            int[] mas2 = new int[mas1.Length];

            int rows = mas1.GetUpperBound(0) + 1;
            int columns = mas1.Length / rows;
            // или так
            // int columns = mas.GetUpperBound(1) + 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{mas1[i, j]} \t");
                }
                Console.WriteLine();
            }
        }
    }
}
