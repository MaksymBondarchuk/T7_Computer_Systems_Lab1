using System;
using System.Collections.Generic;
using System.Threading;
using T7_Computer_Systems_Lab1.Properties;

namespace T7_Computer_Systems_Lab1
{
    public class Divided
    {
        private readonly List<Thread> _units = new List<Thread>();
        private List<List<int>> _mA;
        private readonly List<List<int>> _mC = new List<List<int>>();
        private const int TactLength = 200;

        private readonly List<int> _freeRows = new List<int>();

        public void unit_work()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                lock (_freeRows)
                {
                    if (_freeRows.Count == 0)
                        return;

                    //Console.WriteLine(Thread.CurrentThread.Name + " taked " + work_with_row.ToString());

                    for (int i = 0; i < _mA[_freeRows[0]].Count; i++)
                        _mC[i][_freeRows[0]] = _mA[_freeRows[0]][i];

                    _freeRows.Remove(_freeRows[0]);

                    Thread.Sleep(TactLength);
                }
            }
        }

        public List<List<int>> Transposition(List<List<int>> mA, int unitsNumber)
        {
            // Removing previous data ---------------
            _mC.Clear();
            _freeRows.Clear();
            _units.Clear();

            // Initialisation -----------------------
            for (var i = 0; i < mA[0].Count; i++)
            {
                _mC.Add(new List<int>());
                for (int j = 0; j < mA.Count; j++)
                    _mC[i].Add(0);
            }

            for (var i = 0; i < mA.Count; i++)
                _freeRows.Add(i);

            _mA = mA;

            for (var i = 0; i < unitsNumber; i++)
            {
                var t = new Thread(unit_work) {Name = i.ToString()};
                _units.Add(t);
            }

            // Work ----------------------------------
            foreach (var t in _units)
                t.Start();

            foreach (var t in _units)
                t.Join();

            return _mC;
        }


        public void print_matrix(List<List<int>> matrix)
        {
            foreach (var t in matrix)
            {
                foreach (var t1 in t)
                    Console.Write(Resources.Divided_print_matrix__0_4_, t1);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
