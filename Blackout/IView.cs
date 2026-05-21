using System;
using System.Collections.Generic;

namespace Blackout.View
{
    public interface IView
    {
        // void DrawBoard(Grid grid, int selectedRow, int selectedCol);
        void UpdateGrid(Grid grid);
        int SelectDifficulty();
        void ShowVictory();
        int ShowMenu();
        (int row, int col) AskCoordinates();
    }
}