using System;

namespace Blackout
{
    /// <summary>
    /// Controls the game flow and coordinates interaction between
    /// the Model (Grid) and the View.
    /// </summary>
    public class Controller
    {
        private Grid grid;

        /// <summary>
        /// Runs the main game loop until the victory condition is met.
        /// </summary>
        public void Run(/* IView view*/)
        {
            //int size = view.MenuView();
            //if (size != null) CreateGrid(size);

            // Main game loop
            do
            {
                //view.ShowGrid(grid);

            }while (!grid.IsVictory());
        }

        /// <summary>
        /// Creates a new square grid with specified size.
        /// </summary>
        /// <param name="size">Number of rows and columns of the grid.</param>
        public void CreateGrid(int size)
        {
            grid = new Grid(size, size);
        }
    }
}