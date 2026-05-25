using System;

namespace Blackout
{
    /// <summary>
    /// Represents a single cell in the grid. Each cell stores its current
    /// state (ON or OFF) and exposes it to the rest of the game logic.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets or sets the current state of the cell.
        /// </summary>
        public CellState State { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class
        /// with the specified initial state. 
        /// </summary>
        /// <param name="state">The starting state of the cell.</param>
        public Cell(CellState state)
        {
            State = state;
        }
    }
}