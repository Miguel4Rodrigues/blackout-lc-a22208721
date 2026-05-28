using System;

namespace Blackout
{
    /// <summary>
    /// Represents a 2D grid of cells used in the Blackout puzzle.
    /// Handles cell initialization, state toggling, and victory checking.
    /// </summary>
    public class Grid
    {
        private Cell[ , ] cells;

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        public int Rows { get; }
        /// <summary>
        /// Gets the number of columns in the grid.
        /// </summary>
        public int Columns { get; }
        private static readonly Random rnd = new Random();


        /// <summary>
        /// Creates a new grid with the specified number of rows and columns.
        /// All cells are initialized in the OFF state.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
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
        /// Applies a number of random toggles to generate the initial 
        /// puzzle configuration. Each toggle affects a cell and its
        /// Von Neumann neighbors.
        /// </summary>
        /// <param name="clicks">The number of random toggles to apply.</param>
        public void ApplyRandomClicks(int clicks)
        {
            for (int i = 0; i < clicks; i++)
            {
                int r = rnd.Next(Rows);
                int c = rnd.Next(Columns);

                ToggleVonNeumann(r, c);
            }
        }
        
        /// <summary>
        /// Toggle the state of the selected cell and its 
        /// eight orthogonal and diagonal neighbors (Von Neumann neighborhood).
        /// </summary>
        /// <param name="row">The row index of the central cell.</param>
        /// <param name="column">The column index of the central cell.</param>
        public void ToggleVonNeumann(int row, int column)
        {
            // Toggle the cell itself
            Toggle(row, column);

            // Toggle neighbors
            Toggle(row - 1, column); // UP
            Toggle(row + 1, column); // DOWN
            Toggle(row, column - 1); // LEFT
            Toggle(row, column + 1); // RIGHT
            Toggle(row + 1, column + 1); // DOWN-RIGHT
            Toggle(row - 1, column + 1); // UP-RIGHT
            Toggle(row + 1, column - 1); // DOWN-LEFT
            Toggle(row - 1, column - 1); // UP-LEFT
        }

        /// <summary>
        /// Toggles the state of the cell at the given coordinates,
        /// if the coordinates are within the grid boundaries.
        /// </summary>
        /// <param name="r">The row index of the cell.</param>
        /// <param name="c">The column index of the cell.</param>
        public void Toggle(int r, int c)
        {
            if (r < 0 || r >= Rows || c < 0 || c >= Columns) return;

            if (cells[r, c].State == CellState.ON)
                cells[r, c].State = CellState.OFF;
            else
                cells[r, c].State = CellState.ON;
        }

        /// <summary>
        /// Determines whether the puzzle has been solved.
        /// The puzzle is solved when all cells are OFF.
        /// </summary>
        /// <returns> True if all cells are OFF; otherwise, False.</returns>
        public bool IsVictory()
        {
            foreach (Cell cell in cells)
            {
                if (cell.State == CellState.ON)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves the cell at the specified row and column.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="col">The column index of the cell</param>
        /// <returns>The <see cref="Cell"/> at the given coordinates.</returns>
        
        public Cell GetCell(int row, int col)
        {
            return cells[row, col];
        }
    }
}