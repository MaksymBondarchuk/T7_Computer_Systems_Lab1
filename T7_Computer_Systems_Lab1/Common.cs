using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace T7_Computer_Systems_Lab1
{
    public class Common : Divided
    {
        private readonly List<List<int>> _freeRowsForUnits = new List<List<int>>();
        private readonly List<List<FreeCell>> _freeCellsForUnits = new List<List<FreeCell>>();

        private async Task UnitWork(int id)
        {
            while (true)
            {
                if (Addition || Multiplication)
                {
                    if (_freeCellsForUnits[id].Count == 0)
                        return;

                    var cell = _freeCellsForUnits[id][0];
                    _freeCellsForUnits[id].RemoveAt(0);

                    if (Addition)
                    {
                        Mc[cell.Row][cell.Coll] = Ma[cell.Row][cell.Coll] + Mb[cell.Row][cell.Coll];
                        await Task.Delay(TactLength);
                    }
                    else
                    {
                        for (var i = 0; i < Ma[0].Count; i++)
                            Mc[cell.Row][cell.Coll] += Ma[cell.Row][i]*Mb[i][cell.Coll];
                        var delay = TactLength * (Ma[0].Count * Alpha + Ma[0].Count - 1);
                        await Task.Delay(delay);
                    }

                    Thread.Sleep(TactLength);
                    continue;
                }


                if (!Transposition) continue;

                if (_freeRowsForUnits[id].Count == 0)
                    return;

                var row = _freeRowsForUnits[id][0];
                _freeRowsForUnits[id].RemoveAt(0);
                
                for (var i = 0; i < Ma[row].Count; i++)
                    Mc[i][row] = Ma[row][i];

                Thread.Sleep(TactLength);
            }
        }

        public new async Task<IEnumerable<List<int>>> Add(List<List<int>> mA, List<List<int>> mB, int unitsNumber)
        {
            StartTime = DateTime.Now;
            CommonInitialisation();
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

            return await DoWork(unitsNumber);
        }

        public new async Task<IEnumerable<List<int>>> Multiplicate(List<List<int>> mA, List<List<int>> mB, int unitsNumber, int alpha)
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

            Ma = CopyMatrix(mA);
            Mb = CopyMatrix(mB);

            for (var i = 0; i < unitsNumber; i++)
                _freeCellsForUnits.Add(new List<FreeCell>());

            var currentUnit = 0;
            for (var i = 0; i < Mc.Count; i++)
                for (var j = 0; j < Mc[i].Count; j++)
                    _freeCellsForUnits[currentUnit++ % unitsNumber].Add(new FreeCell(i, j));

            return await DoWork(unitsNumber);
        }

        public new async Task<IEnumerable<List<int>>> Transpose(List<List<int>> mA, int unitsNumber)
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

            Ma = CopyMatrix(mA);

            for (var i = 0; i < unitsNumber; i++)
                _freeRowsForUnits.Add(new List<int>());

            var currentUnit = 0;
            for (var i = 0; i < Ma.Count; i++)
                _freeRowsForUnits[currentUnit++ % unitsNumber].Add(i);

            return await DoWork(unitsNumber);
        }

        private async Task<IEnumerable<List<int>>> DoWork(int unitsNumber)
        {
            for (var i = 0; i < unitsNumber; i++)
                Units.Add(UnitWork(i));
            await Task.WhenAll(Units);

            Time = DateTime.Now - StartTime;
            return Mc;
        }

        private void CommonInitialisation()
        {
            Mc.Clear();
            _freeRowsForUnits.Clear();
            _freeCellsForUnits.Clear();
            Units.Clear();
            Addition = false;
            Transposition = false;
            Multiplication = false;
        }
    }
}
