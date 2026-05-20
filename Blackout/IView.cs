using System;
using System.Collections.Generic;

namespace Blackout.View
{
    public interface IView
    {
        void DrawBoard(Grid grid);
        int SelectDifficulty();
        void ShowVictory();
        void ShowMenu();
        (int row, int col) AskCoordinates();
    }
}