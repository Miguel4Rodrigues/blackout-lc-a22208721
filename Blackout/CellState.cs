namespace Blackout
{
    /// <summary>
    /// Represents the possible states of a cell in the Blackout grid.
    /// A cell can be either turned ON or OFF.
    /// </summary>
    public enum CellState
    {
        /// <summary>
        /// The cell is active (lit).
        /// </summary>
        ON,
        
        /// <summary>
        /// The cell is inactive (unlit).
        /// </summary>
        OFF
    }
}