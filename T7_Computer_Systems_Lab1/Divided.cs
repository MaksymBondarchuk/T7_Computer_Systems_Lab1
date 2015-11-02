using System;
using System.Collections.Generic;
using System.Threading;

namespace T7_Computer_Systems_Lab1
{
    public class Divided
    {
        protected readonly List<Thread> Units = new List<Thread>();
        protected List<List<int>> Ma;
        protected List<List<int>> Mb;
        protected List<List<int>> Mc = new List<List<int>>();
        protected const int TactLength = 200;

        protected bool Addition;
        protected bool Multiplication;
        protected bool Transposition;

        private readonly List<int> _freeRows = new List<int>();

        public TimeSpan Time;
        protected DateTime StartTime;

        protected int Alpha;

        protected class FreeCell
        {
            public readonly int Row;
            public readonly int Coll;

            public FreeCell(int row, int coll)
            {
                Row = row;
                Coll = coll;
            }
        }
        protected readonly List<FreeCell> FreeCells = new List<FreeCell>();

        private void UnitWork()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                if (Addition || Multiplication)
                {
                    FreeCell cell;
                    lock (FreeCells)
                    {
                        if (FreeCells.Count == 0)
                            return;
                        cell = FreeCells[0];
                        FreeCells.RemoveAt(0);
                    }

                    if (Addition)
                        Mc[cell.Row][cell.Coll] = Ma[cell.Row][cell.Coll] + Mb[cell.Row][cell.Coll];
                    else
                        for (var i = 0; i < Ma[0].Count; i++)
                            Mc[cell.Row][cell.Coll] += Ma[cell.Row][i] * Mb[i][cell.Coll];

                    Thread.Sleep(TactLength);
                    continue;
                }


                if (!Transposition) continue;
                int row;
                lock (_freeRows)
                {
                    if (_freeRows.Count == 0)
                        return;
                    row = _freeRows[0];
                    _freeRows.RemoveAt(0);
                }

                for (var i = 0; i < Ma[row].Count; i++)
                    Mc[i][row] = Ma[row][i];

                Thread.Sleep(TactLength);
            }
        }

        public IEnumerable<List<int>> Add(List<List<int>> mA, List<List<int>> mB, int unitsNumber, int alpha)
        {
            StartTime = DateTime.Now;
            Alpha = alpha;
            CommonInitialisation(unitsNumber);
            Addition = true;

            for (var i = 0; i < mA.Count; i++)
                for (var j = 0; j < mA[i].Count; j++)
                    FreeCells.Add(new FreeCell(i, j));

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);
            Mc = CopyMatrix(mA);

            return DoWork();
        }

        public IEnumerable<List<int>> Multiplicate(List<List<int>> mA, List<List<int>> mB, int unitsNumber, int alpha)
        {
            StartTime = DateTime.Now;
            Alpha = alpha;
            CommonInitialisation(unitsNumber);
            Multiplication = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA.Count; i++)
            {
                Mc.Add(new List<int>());
                for (var j = 0; j < mB[0].Count; j++)
                    Mc[i].Add(0);
            }

            for (var i = 0; i < Mc.Count; i++)
                for (var j = 0; j < Mc[i].Count; j++)
                    FreeCells.Add(new FreeCell(i, j));

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);

            return DoWork();
        }

        public IEnumerable<List<int>> Transpose(List<List<int>> mA, int unitsNumber, int alpha)
        {
            StartTime = DateTime.Now;
            Alpha = alpha;
            CommonInitialisation(unitsNumber);
            Transposition = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA[0].Count; i++)
            {
                Mc.Add(new List<int>());
                for (var j = 0; j < mA.Count; j++)
                    Mc[i].Add(0);
            }

            for (var i = 0; i < mA.Count; i++)
                _freeRows.Add(i);

            Ma = CopyMatrix(mA);

            return DoWork();
        }

        public void PrintMatrix(IEnumerable<List<int>> matrix)
        {
            foreach (var t in matrix)
            {
                foreach (var t1 in t)
                    Console.Write($"{t1,4}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void CommonInitialisation(int unitsNumber)
        {
            Mc.Clear();
            _freeRows.Clear();
            Units.Clear();
            Addition = false;
            Transposition = false;
            Multiplication = false;

            for (var i = 0; i < unitsNumber; i++)
                Units.Add(new Thread(UnitWork) { Name = i.ToString() });
        }

        protected List<List<int>> DoWork()
        {
            foreach (var t in Units)
                t.Start();

            foreach (var t in Units)
                t.Join();

            Time = DateTime.Now - StartTime;
            return Mc;
        }

        protected List<List<int>> CopyMatrix(List<List<int>> from)
        {
            var to = new List<List<int>>();
            for (var i = 0; i < from.Count; i++)
            {
                to.Add(new List<int>());
                for (var j = 0; j < from[i].Count; j++)
                    to[i].Add(from[i][j]);
            }
            return to;
        }
    }
}
