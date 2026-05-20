using System;
using System.Collections.Generic;

namespace Blackout.View
{
    public interface IView
    {
        void DrawBoard(bool[,] aBoard);
        int SelectDifficulty();
        void ShowVictory();
        void ShowMenu();
        (int row, int col) AskCoordinates();
    }
}