using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace T7_Computer_Systems_Lab1
{
    public class Divided
    {
        private List<Thread> units = new List<Thread>();
        private List<List<int>> mA;
        private List<List<int>> mC = new List<List<int>>();
        private int tact_length = 200;

        private List<int> free_rows = new List<int>();

        public void unit_work()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                lock (free_rows)
                {
                    if (free_rows.Count == 0)
                        return;

                    int work_with_row = free_rows[0];


                    //Console.WriteLine(Thread.CurrentThread.Name + " taked " + work_with_row.ToString());

                    for (int i = 0; i < mA[free_rows[0]].Count; i++)
                        mC[i][free_rows[0]] = mA[free_rows[0]][i];

                    free_rows.Remove(free_rows[0]);

                    Thread.Sleep(tact_length);
                }
            }
        }

        public List<List<int>> Transposition(List<List<int>> mA, int units_number)
        {
            // Removing previous data ---------------
            mC.Clear();
            free_rows.Clear();
            units.Clear();

            // Initialisation -----------------------
            for (int i = 0; i < mA[0].Count; i++)
            {
                mC.Add(new List<int>());
                for (int j = 0; j < mA.Count; j++)
                    mC[i].Add(0);
            }

            for (int i = 0; i < mA.Count; i++)
                free_rows.Add(i);

            this.mA = mA;

            for (int i = 0; i < units_number; i++)
            {
                Thread t = new Thread(new ThreadStart(unit_work));
                t.Name = i.ToString();
                units.Add(t);
            }

            // Work ----------------------------------
            for (int i = 0; i < units.Count; i++)
                units[i].Start();

            for (int i = 0; i < units.Count; i++)
                units[i].Join();

            return mC;
        }


        private void Reset()
        {

        }

        public void print_matrix(List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                    Console.Write(string.Format("{0,4}", matrix[i][j]));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
