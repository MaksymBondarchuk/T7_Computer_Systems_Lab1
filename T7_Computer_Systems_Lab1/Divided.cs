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

        private int tact_nmb = -1;

        private List<int> free_rows = new List<int>();

        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private static Mutex mtx = new Mutex();
        private bool flag_mutex = true;

        System.IO.StreamWriter logfile = new System.IO.StreamWriter("log.log");

        // Will transpose mA
        public Divided(List<List<int>> mA, int units_number)
        {
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
                //units.Add(new Thread(() => unit_work(i)));
            }

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(tact_length);

            logfile.WriteLine("All created\n");
        }

        public void unit_work()
        {
            //int my_tact_nmb = -1;
            Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                //if (my_tact_nmb < tact_nmb)
                //{
                lock(free_rows)
                {
                    //mtx.WaitOne();
                    //Console.WriteLine(Thread.CurrentThread.Name + " alive");


                    if (free_rows.Count == 0)
                        return;

                    int work_with_row = free_rows[0];
                    

                    Console.WriteLine(Thread.CurrentThread.Name + " taked " + work_with_row.ToString());

                    for (int i = 0; i < mA[free_rows[0]].Count; i++)
                        mC[i][free_rows[0]] = mA[free_rows[0]][i];

                    free_rows.Remove(free_rows[0]);
                    //mtx.ReleaseMutex();
                    //my_tact_nmb++;
                    //}

                    Thread.Sleep(tact_length);
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (free_rows.Count == 0)
                for (int i = 0; i < units.Count; i++)
                    units[i].Abort();

            //if (0 < free_rows.Count)
            //    tact_nmb++;
            //else
            //{
            //    dispatcherTimer.Stop();
            //    for (int i = 0; i < units.Count; i++)
            //        units[i].Abort();
            //}
        }

        public List<List<int>> Transposition()
        {
            for (int i = 0; i < units.Count; i++)
                units[i].Start();

            dispatcherTimer.Start();

            for (int i = 0; i < units.Count; i++)
                units[i].Join();
            dispatcherTimer.Stop();
            logfile.Close();
            return mC;
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
