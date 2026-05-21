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

        /// <summary>
        /// Runs the main game loop until the victory condition is met.
        /// </summary>
        public void Run(IView view)
        {
            size = GetGameSize(view);
            if (size == -1) return;

            CreateGrid(size);
            //view.StartGrid(grid);

            // Main game loop
            do
            {   
                view.UpdateGrid(grid, selectedRow, selectedCol); 
                /*ConsoleKey key = Console.ReadKey(true).Key; // CRIAR MÉTODO NA VIEW que retorna a key (ou seja lê a tecla que o utilizador selecionou)
                HandleInput(key);*/


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
            }
        }
    }
}