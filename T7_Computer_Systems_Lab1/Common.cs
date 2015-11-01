using System;
using System.Collections.Generic;
using System.Threading;

namespace T7_Computer_Systems_Lab1
{
    public class Common : Divided
    {
        private readonly List<List<int>> _freeRowsForUnits = new List<List<int>>();
        private readonly List<List<FreeCell>> _freeCellsForUnits = new List<List<FreeCell>>();

        public new void UnitWork()
        {
            //Console.WriteLine(Thread.CurrentThread.Name + " started");
            var myId = Convert.ToInt32(Thread.CurrentThread.Name);

            while (true)
            {
                if (Addition || Multiplication)
                {
                    if (_freeCellsForUnits[myId].Count == 0)
                        return;

                    var cell = _freeCellsForUnits[myId][0];
                    _freeCellsForUnits[myId].RemoveAt(0);

                    if (Addition)
                        Mc[cell.Row][cell.Coll] = Ma[cell.Row][cell.Coll] + Mb[cell.Row][cell.Coll];
                    else
                        for (var i = 0; i < Ma[0].Count; i++)
                            Mc[cell.Row][cell.Coll] += Ma[cell.Row][i] * Mb[i][cell.Coll];

                    Thread.Sleep(TactLength);
                    continue;
                }


                if (!Transposition) continue;

                if (_freeRowsForUnits[myId].Count == 0)
                    return;

                var row = _freeRowsForUnits[myId][0];
                _freeRowsForUnits[myId].RemoveAt(0);
                
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

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);
            Mc = CopyMatrix(mA);

            for (var i = 0; i < unitsNumber; i++)
                _freeCellsForUnits.Add(new List<FreeCell>());

            var currentUnit = 0;
            for (var i = 0; i < Ma.Count; i++)
                for (var j = 0; j < Ma[i].Count; j++)
                    _freeCellsForUnits[currentUnit++ % unitsNumber].Add(new FreeCell(i, j));

            return DoWork();
        }

        public new List<List<int>> Multiplicate(List<List<int>> mA, List<List<int>> mB, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation(unitsNumber);
            Multiplication = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA.Count; i++)
            {
                Mc.Add(new List<int>());
                for (var j = 0; j < mB[0].Count; j++)
                    Mc[i].Add(0);
            }

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);

            for (var i = 0; i < unitsNumber; i++)
                _freeCellsForUnits.Add(new List<FreeCell>());

            var currentUnit = 0;
            for (var i = 0; i < Mc.Count; i++)
                for (var j = 0; j < Mc[i].Count; j++)
                    _freeCellsForUnits[currentUnit++ % unitsNumber].Add(new FreeCell(i, j));

            return DoWork();
        }

        public new List<List<int>> Transpose(List<List<int>> mA, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation(unitsNumber);
            Transposition = true;

            // Result matrix initialisation -----------------------
            for (var i = 0; i < mA[0].Count; i++)
            {
                Mc.Add(new List<int>());
                for (var j = 0; j < mA.Count; j++)
                    Mc[i].Add(0);
            }

            Ma = CopyMatrix(mA);

            for (var i = 0; i < unitsNumber; i++)
                _freeRowsForUnits.Add(new List<int>());

            var currentUnit = 0;
            for (var i = 0; i < Ma.Count; i++)
                _freeRowsForUnits[currentUnit++ % unitsNumber].Add(i);

            return DoWork();
        }

        protected new void CommonInitialisation(int unitsNumber)
        {
            Mc.Clear();
            _freeRowsForUnits.Clear();
            _freeCellsForUnits.Clear();
            Units.Clear();
            Addition = false;
            Transposition = false;
            Multiplication = false;

            for (var i = 0; i < unitsNumber; i++)
                Units.Add(new Thread(UnitWork) { Name = i.ToString() });
        }
    }
}
