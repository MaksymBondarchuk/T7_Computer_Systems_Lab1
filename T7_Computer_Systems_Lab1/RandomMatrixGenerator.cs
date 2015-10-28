using System;
using System.Collections.Generic;

namespace T7_Computer_Systems_Lab1
{
    public class RandomMatrixGenerator
    {
        readonly Random _rand = new Random();

        public List<List<int>> Generate(int rows, int cols)
        {
            var matrix = new List<List<int>>();

            for (var i = 0; i < rows; i++)
            {
                matrix.Add(new List<int>(cols));
                for (var j = 0; j < cols; j++)
                    matrix[i].Add(_rand.Next(-9, 10));
            }

            return matrix;
        }
    }
}
