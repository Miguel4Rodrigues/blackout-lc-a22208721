using System;

namespace Blackout
{
    public class Cell
    {
        public CellState State { get; set; }
        public Cell(CellState state)
        {
            State = state;
        }
    }
}