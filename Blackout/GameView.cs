using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Blackout.View
{
    /// <summary>
    /// Represents the game view, responsible for displaying the board, menus, and handling user input. Uses Spectre.Console for improved console output.
    /// </summary>
    public class GameView
    {
        private Canvas canvas;
        public void StartGrid(Grid grid)
        {
            int cellSize = 2;
            int width = grid.Columns * cellSize + grid.Columns + 1;
            int height = grid.Rows * cellSize + grid.Rows + 1;
            canvas = new Canvas(width, height);
        }

        public void UpdateGrid(Grid grid, int selectedRow, int selectedCol)
        {
            int cellSize = 2;
            int width = grid.Columns * cellSize + grid.Columns + 1;
            int height = grid.Rows * cellSize + grid.Rows + 1;
            canvas = new Canvas(width, height);

            AnsiConsole.Clear();
            ShowInstructions();
            
            // Draw grid lines
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x % (cellSize + 1) == 0 || y % (cellSize + 1) == 0)
                        canvas.SetPixel(x, y, Color.Black);
                }
            }
            
            // Draw cells
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
                {
                    Cell cell = grid.GetCell(row, col);
                    Color color = cell.State == CellState.ON ? Color.Yellow : Color.Grey;
                    
                    if (row == selectedRow && col == selectedCol)
                        color = Color.Red;
                    
                    int startX = col * (cellSize + 1) + 1;
                    int startY = row * (cellSize + 1) + 1;
                    
                    for (int dx = 0; dx < cellSize; dx++)
                    {
                        for (int dy = 0; dy < cellSize; dy++)
                        {
                            canvas.SetPixel(startX + dx, startY + dy, color);
                        }
                    }
                }
            }
            AnsiConsole.Write(canvas);
        }

        /// <summary>
        /// Displays the main menu of the game, allowing the user to start a new game or exit.
        /// </summary>
        public int ShowMenu()
        {
            Panel panel = new Panel("[bold green]BLACKOUT[/]")
                .Border(BoxBorder.Double)
                .Padding(1, 1)
                .Expand();

            AnsiConsole.Write(panel);

            AnsiConsole.WriteLine();

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices("[cyan]1[/] - Start New Game", "[cyan]2[/] - Exit")
            );
            if (choice == "[cyan]1[/] - Start New Game") return 1;
            if (choice == "[cyan]2[/] - Exit") return 2;
            
            Console.WriteLine("Invalid choice. Defaulting to Exit.");
            return 2;
        }
        /// <summary>
        /// Displays instructions for the game controls to the user.
        /// </summary>
        public void ShowInstructions()
        {
            AnsiConsole.MarkupLine("[bold][White]Arrows = move | Space = select | ESC = exit[/][/]");
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Asks the user to select a difficulty level and returns the corresponding board size.
        /// </summary>
        /// <returns>
        /// The size of the board corresponding to the selected difficulty level.
        /// </returns>
        public int SelectDifficulty()
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]difficulty[/]:")
                    .AddChoices("1 - Easy (3x3)", "2 - Medium (5x5)", "3 - Hard (8x8)")
            );

            if (choice == "1 - Easy (3x3)") return 3;
            if (choice == "2 - Medium (5x5)") return 5;
            if (choice == "3 - Hard (8x8)") return 8;

            Console.WriteLine("Invalid choice. Defaulting to Easy.");
            return 3;
        }
        
        /// <summary>
        /// Reads a key input from the user and returns it as a ConsoleKey.
        /// </summary>
        /// <returns>The ConsoleKey corresponding to the user's input.</returns>
        public ConsoleKey ReadInputPlayer() => Console.ReadKey(true).Key;

        /// <summary>
        /// Displays a victory message to the user when they win the game.
        /// </summary>
        public void ShowVictory()
        {
            AnsiConsole.Write(
                new Panel("[bold green]Victory![/]")
                    .Border(BoxBorder.Double)
                    .Padding(2, 1)
            );
        }
    }
}