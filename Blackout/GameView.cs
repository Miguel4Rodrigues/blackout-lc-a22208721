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
        /*
        /// <summary>
        /// Draws the game board to the console, using different colors for cells. ON (yellow) and OFF (grey).
        /// </summary>
        /// <param name="grid"></param>
        public void DrawBoard (Grid grid, int selectedRow, int selectedCol)
        {
            AnsiConsole.WriteLine();

            for(int row = 0; row < grid.Rows; row++)
            {
                for(int col = 0; col < grid.Columns; col++)
                {
                    Cell cell = grid.GetCell(row, col);
                    if (row == selectedRow && col == selectedCol)
                        AnsiConsole.Markup("[red]■ [/]");
                    else if (cell.State == CellState.ON)
                        AnsiConsole.Markup("[yellow]■ [/]");
                    else
                        AnsiConsole.Markup("[grey]■ [/]");
                }
                AnsiConsole.WriteLine();
            }
            AnsiConsole.WriteLine();
        }
        */
        public void StartGrid(Grid grid)
        {
            canvas = new Canvas(grid.Columns, grid.Rows);
            
            AnsiConsole.Live(canvas)
                .AutoClear(false)
                .Start(ctx =>
                {
                    ctx.Refresh();
                });
        }

        public void UpdateGrid(Grid grid, int selectedRow, int selectedCol)
        {
            AnsiConsole.Clear();
        
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
                {
                    Cell cell = grid.GetCell(row, col);
                    Color color = cell.State == CellState.ON ? Color.Yellow : Color.Grey;
                    
                    if (row == selectedRow && col == selectedCol)
                        color = Color.Red;
                    
                    canvas.SetPixel(col, row, color);
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

        /// <summary>
        /// Asks the user to input the row and column coordinates for their move, and returns them as a tuple.
        /// </summary>
        /// <returns>
        /// A tuple containing the selected row and column coordinates.
        /// </returns>
        public (int row, int col) AskCoordinates()
        {
            int row = AnsiConsole.Ask<int>("Row:");
            int col = AnsiConsole.Ask<int>("Col:");

            return (row, col);
        }
    }
}