using System;
using Spectre.Console;

namespace Blackout.View
{
    /// <summary>
    /// Represents the game view, responsible for rendering the grid,
    /// menus, instructions, and reading user input. Uses Spectre.Console
    /// for enhanced console visualization.
    /// </summary>
    public class GameView
    {
        private Canvas canvas;
        private int width;
        private int height;

        /// <summary>
        /// Scale the cell
        /// </summary>
        private int cellSize = 2;

        /// <summary>
        /// Initializes the drawing canvas based on the grid dimensions.
        /// Must be called before updating or rendering the grid.
        /// </summary>
        /// <param name="grid">The grid whose dimensions determine the canvas size.</param>
        public void StartGrid(Grid grid)
        {
            width = grid.Columns * cellSize + grid.Columns + 1;

            height = grid.Rows * cellSize + grid.Rows + 1;

            canvas = new Canvas(width, height);
        }

        /// <summary>
        /// Renders the current state of the grid, including cell colors,
        /// grid lines, and the selected cell highlight.
        /// </summary>
        /// <param name="grid">The grid containing the cell states to display.</param>
        /// <param name="selectedRow">The row index of the currently selected cell.</param>
        /// <param name="selectedCol">The column index of the currently selected cell.</param>
        public void UpdateGrid(Grid grid, int selectedRow, int selectedCol)
        {
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
        /// Displays the main menu of the game, allowing the user to start a new game.
        /// </summary>
        /// <returns>The selected menu option as a string.</returns>
        public string ShowMenu()
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
            return choice;
        }

        /// <summary>
        /// Displays a farewell message when the user exits the game.
        /// </summary>
        public void ShowExitMessage()
        {
            AnsiConsole.MarkupLine("[bold][White]Bye![/][/]");
        }

        /// <summary>
        /// Displays instructions for the game controls.
        /// </summary>
        public void ShowInstructions()
        {
            AnsiConsole.MarkupLine("[bold][White]Arrows = move | Space = select | ESC = exit[/][/]");
        }

        /// <summary>
        /// Prompts the user to select a difficulty level.
        /// </summary>
        /// <returns>A string representing the selected difficulty option.</returns>
        public string SelectDifficulty()
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]difficulty[/]:")
                    .AddChoices("1 - Easy (3x3)", "2 - Medium (5x5)", "3 - Hard (8x8)")
            );

            return choice;
        }
        
        /// <summary>
        /// Reads a key press from the user without displaying it on the console.
        /// </summary>
        /// <returns>The key pressed by the user.</returns>
        public ConsoleKey ReadInputPlayer() => Console.ReadKey(true).Key;

        /// <summary>
        /// Displays a victory message whe the player wins the game.
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