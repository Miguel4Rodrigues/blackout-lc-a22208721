using System;

namespace Blackout
{
    public class Cell
    {
        public Cell Up;
        public Cell Down;
        public Cell Left;
        public Cell Right;
        
        public CellState State { get; set; }

        public Cell(CellState state)
        {

            State = state;
        }
    }
}