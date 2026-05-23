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
        private int selectedRow = 0;
        private int selectedCol = 0;
        private int size;

        private bool isRunning = true;

        /// <summary>
        /// Runs the main game loop until the victory condition is met.
        /// </summary>
        public void Run(GameView view)
        {
            string option;
            option = view.ShowMenu();

            switch(option)
            {
                case "[cyan]1[/] - Start New Game":
                    GetGameSize(view);
                    CreateGrid(size);
                    view.StartGrid(grid);
                break;

                case "[cyan]2[/] - Exit":
                    view.ShowExitMessage();
                    return;

            }

            // Main game loop
            do
            {   
                view.UpdateGrid(grid, selectedRow, selectedCol); 
                HandleInput(view.ReadInputPlayer());
                
            }while (!grid.IsVictory() && isRunning);


            if (grid.IsVictory())
                view.ShowVictory();
            else
                view.ShowExitMessage();
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

        private int GetGameSize(GameView view)
        {
            switch (view.SelectDifficulty())
            {
                case "1 - Easy (3x3)":
                    size = 3;
                    break;
                case "2 - Medium (5x5)":
                    size = 5;
                    break;
                case "3 - Hard (8x8)":
                    size = 8;
                    break;
            }

            return size;
        }
        

        private void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedRow = Math.Max(0, selectedRow - 1);
                    break;

                case ConsoleKey.DownArrow:
                    selectedRow = Math.Min(size - 1, selectedRow + 1);
                    break;
                
                case ConsoleKey.RightArrow:
                    selectedCol = Math.Min(size - 1, selectedCol + 1);
                    break;

                case ConsoleKey.LeftArrow:
                    selectedCol = Math.Max(0, selectedCol - 1);
                    break;

                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    grid.ToggleVonNeumann(selectedRow, selectedCol);
                    break;

                case ConsoleKey.Escape:
                    isRunning = false;
                    break;
            }
        }
    }
}