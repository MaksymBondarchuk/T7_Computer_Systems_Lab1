using System;
using System.Collections.Generic;
using System.Threading;

namespace T7_Computer_Systems_Lab1
{
    class Common : Divided
    {
        private readonly List<List<int>> _freeRowsForUnits = new List<List<int>>();
        private readonly List<List<FreeCell>> _freeCellsForUnits = new List<List<FreeCell>>();

        public new void unit_work()
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
                lock (FreeRows)
                {
                    if (FreeRows.Count == 0)
                        return;
                    row = FreeRows[0];
                    FreeRows.RemoveAt(0);
                }

                for (var i = 0; i < Ma[row].Count; i++)
                    Mc[i][row] = Ma[row][i];

                Thread.Sleep(TactLength);
            }
        }

        public new List<List<int>> Add(List<List<int>> mA, List<List<int>> mB, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation(unitsNumber);
            Addition = true;

            var allCells = mA.Count + mA[0].Count;
            var cellsForOneUnit = Convert.ToInt32(allCells/unitsNumber);
            if (unitsNumber*cellsForOneUnit != allCells)
                cellsForOneUnit++;



            for (var i = 0; i < mA.Count; i++)
                for (var j = 0; j < mA[i].Count; j++)
                    FreeCells.Add(new FreeCell(i, j));

            Ma = mA;
            Mb = mB;
            Mc = mA;

            return DoWork();
        }
    }
}
