using System;
using System.Threading;
using Blackout;
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
        /// Defines the size of each cell when rendered on the canvas,
        /// </summary>
        private readonly int cellSize = 2;

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
        /// grid lines, and the selected cell highlight. Also displays instructions and high score information.
        /// </summary>
        /// <param name="grid">The grid containing the cell states to display.</param>
        /// <param name="selectedRow">The row index of the currently selected cell.</param>
        /// <param name="selectedCol">The column index of the currently selected cell.</param>
        /// <param name="difficulty">The current game difficulty.</param>
        /// <param name="highScore">The best score for the current difficulty.</param>
        public void UpdateGrid(Grid grid, int selectedRow, int selectedCol, GameDifficulty difficulty, int? highScore)
        {
            AnsiConsole.Clear();
            ShowInstructions();
            ShowHighScore(difficulty, highScore);

            DrawGridLines();
            DrawCells(grid, selectedRow, selectedCol);

            AnsiConsole.Write(canvas);
        }
        
        /// <summary>
        /// Draws the grid lines on the canvas using black pixels.
        /// </summary>
        private void DrawGridLines()
        {
            // Draw grid lines
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x % (cellSize + 1) == 0 || y % (cellSize + 1) == 0)
                        canvas.SetPixel(x, y, Color.Black);
                }
            }
        }

        /// <summary>
        /// Draws all cells of the grid onto the canvas, including the highlight
        /// for the currently selected cell.
        /// </summary>
        /// <param name="grid">The grid containing the cell states.</param>
        /// <param name="selectedRow">The row index of the selected cell.</param>
        /// <param name="selectedCol">The column index of the selected cell.</param>
        private void DrawCells(Grid grid, int selectedRow, int selectedCol)
        {
            // Draw cells
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
                {
                    Cell cell = grid.GetCell(row, col);
                    Color color;

                    if (cell.State == CellState.ON)
                        color = Color.Yellow;
                    else
                        color = Color.Grey;
                    
                    // Highlight current cell
                    if (row == selectedRow && col == selectedCol)
                        color = Color.Red;
                    
                    int startX = col * (cellSize + 1) + 1;
                    int startY = row * (cellSize + 1) + 1;
                    
                    for (int dx = 0; dx < cellSize; dx++)
                    {
                        for (int dy = 0; dy < cellSize; dy++)
                            canvas.SetPixel(startX + dx, startY + dy, color);
                    }
                }
            }
        }

        /// <summary>
        /// Displays the main menu of the game, allowing the user to start a new game.
        /// </summary>
        /// <returns>The selected menu option as a string.</returns>
        public string ShowMenu()
        {
            AnsiConsole.Write(
                new FigletText("BLACKOUT")
                    .Centered()
                    .Color(Color.Green)
            );

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option[/]")
                    .AddChoices("[cyan]1[/] - Start New Game", "[cyan]2[/] - Exit")
            );

            
            return choice;
        }

        /// <summary>
        /// Displays a farewell message when the user exits the game.
        /// </summary>
        public void ShowExitMessage()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new Panel(
                    "[white]Thank you for playing Blackout.[/]\n\n" +
                    "[yellow]Session terminated.[/]"
                )
                .Header("[bold red]SYSTEM SHUTDOWN[/]")
                .Border(BoxBorder.Double)
                .Padding(1, 1)
            );
        }

        /// <summary>
        /// Displays instructions for the game controls.
        /// </summary>
        public void ShowInstructions()
        {
            AnsiConsole.MarkupLine("[bold White]Arrows = move | Space = select | ESC = exit[/]");
        }

        /// <summary>
        /// Displays the current move count in a styled panel.
        /// </summary>
        /// <param name="moveCount">The number of moves made by the player.</param>
        public void ShowMoveCount(int moveCount)
        {
            AnsiConsole.Write(
                new Panel($"{moveCount}")
                    .Header("[bold blue]Movements[/]")
                    .Border(BoxBorder.Double)
                    .Padding(6, 1)
            );
        }

        /// <summary>
        /// Displays the current high score for the selected difficulty.
        /// </summary>
        /// <param name="difficulty">The current difficulty.</param>
        /// <param name="highScore">The best score for this difficulty.</param>
        public void ShowHighScore(GameDifficulty difficulty, int? highScore)
        {
            string scoreText = highScore.HasValue ? $"{highScore.Value} moves" : "No record yet";

            AnsiConsole.Write(
                new Panel($"[white]Difficulty:[/] [green]{difficulty.GetLabel()}[/]\n[white]Best:[/] [yellow]{scoreText}[/]")
                    .Header("[bold yellow]High Score[/]")
                    .Border(BoxBorder.Double)
                    .Padding(1, 1)
            );
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
        /// Displays a progress bar with the specified message.
        /// Used for visual feedback during operations such as puzzle generation.
        /// </summary>
        /// <param name="message">The message displayed above the progress bar.</param>
        public void ShowProgressBar(string message)
        {
            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask(message);
                    while (!task.IsFinished)
                    {
                        task.Increment(5);
                        Thread.Sleep(50);
                    }
                });
        }
        
        /// <summary>
        /// Reads a key press from the user without displaying it on the console.
        /// </summary>
        /// <returns>The key pressed by the user.</returns>
        public ConsoleKey ReadInputPlayer() => Console.ReadKey(true).Key;

        /// <summary>
        /// Displays a victory message when the player wins, including the move count and whether a new high score was achieved.
        /// </summary>
        /// <param name="moveCount">The number of moves made by the player.</param>
        /// <param name="isNewRecord">Whether the result is a new high score.</param>
        /// <param name="highScore">The best score for this difficulty after the game.</param>
        public void ShowVictory(int moveCount, bool isNewRecord, int? highScore)
        {
            string recordText = isNewRecord ? "[bold green]New record![/]" : $"Best score: [yellow]{highScore?.ToString() ?? "none"}[/] moves.";

            AnsiConsole.Write(
                new Panel($"[bold green]Victory![/] with {moveCount} movements.\n\n{recordText}")
                    .Border(BoxBorder.Double)
                    .Padding(2, 1)
            );
        }
    }
}