using System;
using System.Drawing;
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
            int size = GetGameSize(view);
            if (size == -1) return;

            CreateGrid(size);

            // Main game loop
            do
            {
                view.DrawBoard(grid, selectedRow, selectedCol);

                (selectedRow, selectedCol) = GetValidCoordinates(view, size);

                grid.ToggleVonNeumann(selectedRow, selectedCol);

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

        private int GetGameSize(IView view)
        {
            int option = view.ShowMenu();

            if (option == 2)
                return -1;

            int size = view.SelectDifficulty();

            while (size != 3 && size != 5 && size != 8)
                size = view.SelectDifficulty();

            return size;
        }
        
        private (int row, int col) GetValidCoordinates(IView view, int size)
        {
            (int  row, int  col) = view.AskCoordinates();

            // Validate coordinates
            while (row < 0 || row >= size || col < 0 || col >= size)
            {
                //view.ShowInvalidMessage("Invalid Coordinates!");
                (row, col) = view.AskCoordinates();
            }

            return (row, col);
        }
    }
}