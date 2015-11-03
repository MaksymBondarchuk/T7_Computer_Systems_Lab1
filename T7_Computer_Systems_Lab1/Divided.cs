using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace T7_Computer_Systems_Lab1
{
    public class Divided
    {
        protected readonly List<Task> Units = new List<Task>();
        protected List<List<int>> Ma;
        protected List<List<int>> Mb;
        protected List<List<int>> Mc = new List<List<int>>();
        protected const int TactLength = 50;

        protected bool Addition;
        protected bool Multiplication;
        protected bool Transposition;

        private readonly List<int> _freeRows = new List<int>();

        public TimeSpan Time;
        protected DateTime StartTime;

        protected int Alpha;

        protected int TotalWork;
        public int Percents;
        //private readonly System.IO.StreamWriter _file = new System.IO.StreamWriter("trace.txt");

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
        private readonly List<FreeCell> _freeCells = new List<FreeCell>();

        private async Task UnitWork()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");

            while (true)
            {
                if (Addition || Multiplication)
                {
                    FreeCell cell;
                    lock (_freeCells)
                    {
                        if (_freeCells.Count == 0)
                            return;
                        cell = _freeCells[0];
                        _freeCells.RemoveAt(0);
                        Percents = _freeCells.Count/TotalWork;
                    }

                    if (Addition)
                    {
                        Mc[cell.Row][cell.Coll] = Ma[cell.Row][cell.Coll] + Mb[cell.Row][cell.Coll];
                        Thread.Sleep(TactLength);
                    }
                    else
                    {
                        for (var i = 0; i < Ma[0].Count; i++)
                            Mc[cell.Row][cell.Coll] += Ma[cell.Row][i]*Mb[i][cell.Coll];
                        var delay = TactLength*(Ma[0].Count*Alpha + Ma[0].Count - 1);
                        await Task.Delay(delay);
                    }
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
                    Percents = _freeRows.Count / TotalWork;
                }

                for (var i = 0; i < Ma[row].Count; i++)
                    Mc[i][row] = Ma[row][i];

                Thread.Sleep(TactLength);
            }
        }

        public async Task<IEnumerable<List<int>>> Add(List<List<int>> mA, List<List<int>> mB, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation();
            Addition = true;

            for (var i = 0; i < mA.Count; i++)
                for (var j = 0; j < mA[i].Count; j++)
                    _freeCells.Add(new FreeCell(i, j));

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);
            Mc = CopyMatrix(mA);
            TotalWork = Ma.Count*Ma.Count;

            return await DoWork(unitsNumber);
        }

        public async Task<IEnumerable<List<int>>> Multiplicate(List<List<int>> mA, List<List<int>> mB, int unitsNumber, int alpha)
        {
            StartTime = DateTime.Now;
            CommonInitialisation();
            Alpha = alpha;
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
                    _freeCells.Add(new FreeCell(i, j));

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);
            TotalWork = Mc.Count * Mc[0].Count;
            return await DoWork(unitsNumber);
        }

        public async Task<IEnumerable<List<int>>> Transpose(List<List<int>> mA, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation();
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
            TotalWork = Ma.Count;

            return await DoWork(unitsNumber);
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

        private void CommonInitialisation()
        {
            Mc.Clear();
            _freeRows.Clear();
            _freeCells.Clear();
            Units.Clear();
            Addition = false;
            Transposition = false;
            Multiplication = false;
        }

        private async Task<IEnumerable<List<int>>> DoWork(int unitsNumber)
        {
            for (var i = 0; i < unitsNumber; i++)
                Units.Add(UnitWork());
            await Task.WhenAll(Units);

            Time = DateTime.Now - StartTime;
            return Mc;
        }

        protected static List<List<int>> CopyMatrix(List<List<int>> from)
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
