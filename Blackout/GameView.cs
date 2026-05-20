using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Blackout.View
{
    /// <summary>
    /// Represents the game view, responsible for displaying the board, menus, and handling user input. Uses Spectre.Console for improved console output.
    /// </summary>
    public class GameView : IView
    {
        /// <summary>
        /// Draws the game board to the console. Each cell is represented as "[X]" if true (lit) and "[ ]" if false (unlit).
        /// </summary>
        /// <param name="aBoard"></param>
        public void DrawBoard (Grid grid)
        {
            for(int row = 0; row < grid.Rows; row++)
            {
                for(int col = 0; col < grid.Columns; col++)
                {
                    Cell cell = grid.GetCell(row, col);
                    if (cell.State == CellState.ON)
                        AnsiConsole.Markup("[yellow]■ [/]");
                    else
                        AnsiConsole.Markup("[grey]■ [/]");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the main menu of the game, allowing the user to start a new game or exit.
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("==== BLACKOUT ===");
            Console.WriteLine("1 - Start Game");
            Console.WriteLine("2 - Exit");
        }

        /// <summary>
        /// Asks the user to select a difficulty level and returns the corresponding board size.
        /// </summary>
        /// <returns>
        /// The size of the board corresponding to the selected difficulty level.
        /// </returns>
        public int SelectDifficulty()
        {
            Console.WriteLine("1 - Easy (3x3)");
            Console.WriteLine("2 - Medium (5x5)");
            Console.WriteLine("3 - Hard (8x8)");

            int choice = int.Parse(Console.ReadLine());
            if (choice == 1) return 3;
            if (choice == 2) return 5;
            if (choice == 3) return 8;

            Console.WriteLine("Invalid choice. Defaulting to Easy.");
            return 3;
        }

        /// <summary>
        /// Displays a victory message to the user when they win the game.
        /// </summary>
        public void ShowVictory()
        {
            AnsiConsole.MarkupLine("[green]Victory![/]");
        }

        /// <summary>
        /// Asks the user to input the row and column coordinates for their move, and returns them as a tuple.
        /// </summary>
        /// <returns>
        /// A tuple containing the selected row and column coordinates.
        /// </returns>
        public (int row, int col) AskCoordinates()
        {
            Console.Write("Row: ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Col: ");
            int col = int.Parse(Console.ReadLine());
            
            return (row, col);
        }
    }
}