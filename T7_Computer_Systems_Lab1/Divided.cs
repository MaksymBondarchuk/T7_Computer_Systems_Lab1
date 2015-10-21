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
        List<Thread> units = new List<Thread>();
        List<List<int>> mA;
        List<List<int>> mC = new List<List<int>>();
        int tact_length = 200;

        int tact_nmb = -1;

        List<int> free_rows = new List<int>();

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        static Mutex mtx = new Mutex();
        bool flag_mutex = true;

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
            int my_tact_nmb = -1;
            Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                if (my_tact_nmb < tact_nmb)
                {
                    mtx.WaitOne();
                    logfile.WriteLine(Thread.CurrentThread.Name + " alive");


                    if (free_rows.Count == 0)
                        return;

                    int work_with_row = free_rows[0];

                    logfile.WriteLine(Thread.CurrentThread.Name + " taked " + work_with_row.ToString());

                    for (int i = 0; i < mA[0].Count; i++)
                        mC[i][free_rows[0]] = mA[free_rows[0]][i];

                    mtx.ReleaseMutex();
                    my_tact_nmb++;
                }

                Thread.Sleep(tact_length / 5);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (0 < free_rows.Count)
                tact_nmb++;
            else
            {
                dispatcherTimer.Stop();
                for (int i = 0; i < units.Count; i++)
                    units[i].Abort();
            }
        }

        public List<List<int>> Transposition()
        {
            for (int i = 0; i < units.Count; i++)
                units[i].Start();

            dispatcherTimer.Start();

            units[0].Join();
            logfile.Close();
            return mC;
        }
    }
}
