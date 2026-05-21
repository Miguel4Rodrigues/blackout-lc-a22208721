using System;
using Blackout.View;

namespace Blackout
{
    public class Program
    {
        /// <summary>
        /// Program begins here.
        /// </summary>
        /// <param name="args">Not used</param>
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
