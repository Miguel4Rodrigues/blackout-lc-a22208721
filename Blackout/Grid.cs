using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout
{
    /// <summary>
    /// Represents a 2D grid of cells used in the Blackout puzzle.
    /// </summary>
    public class Grid
    {
        private Cell[ , ] cells;

        /// <summary>
        /// Number of rows in the grid.
        /// </summary>
        public int Rows { get; }
        /// <summary>
        /// Number of columns in the grid.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Creates a new grid with the specific number of rows and columns.
        /// All cells start in the OFF state.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            cells = new Cell[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    cells[r, c] = new Cell(CellState.OFF);
                }
            }
        }
        
        /// <summary>
        /// Toggle the state of the selected cell and its 
        /// four orthogonal neighbors (Von Neumman neighborhood).
        /// </summary>
        /// <param name="row">Row index of the central cell.</param>
        /// <param name="column">Column index of the central cell.</param>
        public void ToggleVonNeumann(int row, int column)
        {
            // Toggle the cell itself
            Toggle(row, column);
            
            // Toggle neighbors
            Toggle(row - 1, column); // UP
            Toggle(row + 1, column); // DOWN
            Toggle(row, column - 1); // LEFT
            Toggle(row, column + 1); // RIGHT
        }

        /// <summary>
        /// Toggles the state of the cell at the given coordinates,
        /// if the coordinates are inside the grid.
        /// </summary>
        /// <param name="r">Row index.</param>
        /// <param name="c">Column index.</param>
        public void Toggle(int r, int c)
        {
            if (r < 0 || r >= Rows || c < 0 || c >= Columns) return;

            if (cells[r, c].State == CellState.ON)
                cells[r, c].State = CellState.OFF;
            else
                cells[r, c].State = CellState.ON;
        }

        /// <summary>
        /// Checks whether all cells in the grid are OFF.
        /// </summary>
        /// <returns>True if all cells are OFF; otherwise false.</returns>
        public bool IsVictory()
        {
            foreach (Cell cell in cells)
            {
                if (cell.State == CellState.ON)
                    return false;
            }

            return true;
        }
    }
}