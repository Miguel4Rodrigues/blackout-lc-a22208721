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
        private int selectedRow;
        private int selectedCol;
        private int size;

        private bool isRunning;

        /// <summary>
        /// Runs the main game loop, starting from the menu selection
        /// and continuing until the player wins or exits the game.
        /// </summary>
        /// <param name="view">
        /// The view responsible for rendering the UI and reading player input
        /// </param>
        public void Run(GameView view)
        {
            isRunning = true;
            
            string option;
            option = view.ShowMenu();

            switch(option)
            {
                case "[cyan]1[/] - Start New Game":
                    GetGameSize(view);
                    CreateGrid(size);
                    view.StartGrid(grid);
                    view.ShowProgressBar("Generating puzzle...");
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

            // When the player wins, clears the selection highlight and renders
            // the final solved grid before displaying the victory panel.
            if (grid.IsVictory())
            {
                selectedRow = -1;
                selectedCol = -1;
                view.UpdateGrid(grid, selectedRow, selectedCol);
                view.ShowVictory();
            }
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
            selectedRow = 0;
            selectedCol = 0;
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

        /// <summary>
        /// Asks the player to select a difficulty level and returns
        /// the corresponding grid size.
        /// </summary>
        /// <param name="view">The view used to display the difficulty menu.</param>
        /// <returns>The selected grid size (3, 5, or 8).</returns>
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
        
        /// <summary>
        /// Processes player input to move the selection cursor,
        /// toggle cells, or exit the game.
        /// </summary>
        /// <param name="key">The key pressed by the player.</param>
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