using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T7_Computer_Systems_Lab1
{
    public class RandomMatrixGenerator
    {
        public List<List<int>> Generate(int rows, int cols)
        {
            List<List<int>> matrix = new List<List<int>>();
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                matrix.Add(new List<int>(cols));
                for (int j = 0; j < cols; j++)
                    matrix[i].Add(rand.Next(-9, 10));
            }

            return matrix;
        }
    }
}
