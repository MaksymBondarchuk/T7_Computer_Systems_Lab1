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
        private List<List<int>> _mB;
        private List<List<int>> _mC = new List<List<int>>();
        private const int TactLength = 200;

        private bool _addition;
        private bool _transposition;

        private readonly List<int> _freeRows = new List<int>();

        public class FreeCell
        {
            public int Row;
            public int Coll;
        }
        private readonly List<FreeCell> _freeCells = new List<FreeCell>();

        public void unit_work()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                if (_addition)
                    lock (_freeCells)
                    {

                        Thread.Sleep(TactLength);
                        continue;
                    }

                if (!_transposition) continue;
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

        public List<List<int>> Transpose(List<List<int>> mA, int unitsNumber)
        {
            CommonInitialisation(unitsNumber);
            _transposition = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA[0].Count; i++)
            {
                _mC.Add(new List<int>());
                for (var j = 0; j < mA.Count; j++)
                    _mC[i].Add(0);
            }

            for (var i = 0; i < mA.Count; i++)
                _freeRows.Add(i);

            _mA = mA;

            return DoWork();
        }

        public List<List<int>> Add(List<List<int>> mA, List<List<int>> mB, int unitsNumber)
        {
            CommonInitialisation(unitsNumber);
            _transposition = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA[0].Count; i++)
            {
                _mC.Add(new List<int>());
                for (var j = 0; j < mA.Count; j++)
                    _mC[i].Add(0);
            }

            for (var i = 0; i < mA.Count; i++)
                for (var j = 0; j < mA[i].Count; j++)
                    _freeRows.Add(i);

            _mA = mA;
            _mB = mB;
            _mC = mA;

            return DoWork();
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

        private void CommonInitialisation(int unitsNumber)
        {
            _mC.Clear();
            _freeRows.Clear();
            _units.Clear();
            _addition = false;
            _transposition = false;

            for (var i = 0; i < unitsNumber; i++)
                _units.Add(new Thread(unit_work) { Name = i.ToString() });
        }

        private List<List<int>> DoWork()
        {
            foreach (var t in _units)
                t.Start();

            foreach (var t in _units)
                t.Join();

            return _mC;
        }
    }
}
