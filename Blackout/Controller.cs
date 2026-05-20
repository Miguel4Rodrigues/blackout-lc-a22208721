using System;

namespace Blackout
{
    public class Controller
    {
        /// <summary>
        /// Run the program
        /// </summary>
        public void Run()
        {
            /*
            // We keep the user's option here
            string option;

            // Main program loop
            
            do
            {
                
            } while( option) // Jogo termina apenas quando todas as células se encontram desligadas
            */
        }

        public void CreateGrid(int size)
        {
            // Create Model (class Cells or Grid)
            Grid grid = new Grid(size, size);
        }
    }
}