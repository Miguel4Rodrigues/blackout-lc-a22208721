using System;
using Blackout.View;

namespace Blackout
{
    /// <summary>
    /// Controls the game flow and coordinates interaction between
    /// the Model (Grid) and the View.
    /// </summary>
    public class Controller
    {
        private Grid grid;
        private int selectedRow = -1;
        private int selectedCol = -1;
        /// <summary>
        /// Runs the main game loop until the victory condition is met.
        /// </summary>
        public void Run(IView view)
        {
            int option = view.ShowMenu();
            int size = 0;
            switch (option)
            {
                case 1:
                default:
                    // Ask the user for the grid size
                    size = view.SelectDifficulty();
                    break;
                case 2:
                    //view.ExitMessage();
                    break;

            }


            // Validate size (must be 3, 5 or 8)
            while (size != 3 && size != 5 && size != 8)
            {
                //view.ShowInvalidMessage("Invalid Size!");
                size = view.SelectDifficulty();
            }
            CreateGrid(size);

            // Main game loop
            do
            {
                view.DrawBoard(grid, selectedRow, selectedCol);
                (int row, int col) = view.AskCoordinates();

                // Validate coordinates
                while (row < 0 || row >= size || col < 0 || col >= size)
                {
                    //view.ShowInvalidMessage("Invalid Coordinates!");
                    (row, col) = view.AskCoordinates();
                }
                selectedRow = row;
                selectedCol = col;

                grid.ToggleVonNeumann(row, col);

            }while (!grid.IsVictory());

            view.ShowVictory();
        }

        /// <summary>
        /// Creates a new square grid with the specified size and initializes it
        /// with a random starting pattern according to the difficulty level.
        /// </summary>
        /// <param name="size">
        /// The dimension of the grid (number of rows and columns).
        /// Supported sizes: 3x3, 5x5, and 8x8.
        /// </param>
        public void CreateGrid(int size)
        {
            grid = new Grid(size, size);

            int clicks = 3;
            switch (size)
            {
                case 5:
                    clicks = 5;
                    break;
                case 8:
                    clicks = 8;
                    break;
                default:
                    clicks = 3;
                    break;
            };

            grid.ApplyRandomClicks(clicks);
        }
    }
}