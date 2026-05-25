using System;
using Blackout.View;

namespace Blackout
{
    /// <summary>
    /// Entry point of the Blackout game application.
    /// Responsible for initializing the Controller and View
    /// and starting the game loop.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program execution begins here.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        private static void Main(string[] args)
        {
            // Create Controller
            Controller controller = new Controller();

            // Create View
            GameView view = new GameView();

            // Initialize controller
            controller.Run(view);
        }
    }
}
