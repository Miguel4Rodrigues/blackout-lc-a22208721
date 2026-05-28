using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackout
{
    /// <summary>
    /// Represents the different difficulty levels of the game, which determine the size of the grid and the complexity of the puzzle.
    /// </summary>
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Provides extension methods for the GameDifficulty enum to retrieve associated properties like grid size and display labels.
    /// </summary>
    public static class GameDifficultyExtensions
    {
        /// <summary>
        /// Returns the grid size associated with each difficulty level, which is used to create the game grid and determine the number of cells.
        /// </summary>
        /// <param name="difficulty">The game difficulty level.</param>
        /// <returns>The grid size for the specified difficulty.</returns>
        public static int GetSize(this GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Easy => 3,
                GameDifficulty.Medium => 5,
                GameDifficulty.Hard => 8,
                _ => 3,
            };
        }

        /// <summary>
        /// Returns the display label associated with each difficulty level.
        /// </summary>
        /// <param name="difficulty">The game difficulty level.</param>
        /// <returns>The display label for the specified difficulty.</returns>
        public static string GetLabel(this GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Easy => "Easy",
                GameDifficulty.Medium => "Medium",
                GameDifficulty.Hard => "Hard",
                _ => "Easy",
            };
        }
    }

    /// <summary>
    /// Manages the high scores for each difficulty level by loading and saving them to a JSON file in the user's local application data folder.
    /// </summary>
    public class HighScoreManager
    {
        private readonly string filePath;
        private HighScoreData highScores;

        public HighScoreManager()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Blackout");
            Directory.CreateDirectory(folder);
            filePath = Path.Combine(folder, "highscores.json");
            highScores = LoadHighScores();
        }

        /// <summary>
        /// Retrieves the current high score for the specified difficulty level, or null if no score is recorded.
        /// </summary>
        /// <param name="difficulty">The game difficulty level.</param>
        /// <returns>The high score for the specified difficulty, or null if no score is recorded.</returns>
        public int? GetHighScore(GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Easy => highScores.Easy,
                GameDifficulty.Medium => highScores.Medium,
                GameDifficulty.Hard => highScores.Hard,
                _ => null,
            };
        }

        /// <summary>
        /// Attempts to update the high score for the specified difficulty level.
        /// </summary>
        /// <param name="difficulty">The game difficulty level.</param>
        /// <param name="moves">The number of moves to compare against the current high score.</param>
        /// <returns>true if the high score was updated; otherwise, false.</returns>
        public bool TryUpdateHighScore(GameDifficulty difficulty, int moves)
        {
            int? current = GetHighScore(difficulty);
            if (!current.HasValue || moves < current.Value)
            {
                switch (difficulty)
                {
                    case GameDifficulty.Easy:
                        highScores.Easy = moves;
                        break;
                    case GameDifficulty.Medium:
                        highScores.Medium = moves;
                        break;
                    case GameDifficulty.Hard:
                        highScores.Hard = moves;
                        break;
                }

                SaveHighScores();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads the high scores from the JSON file. If the file does not exist or cannot be read, returns a new HighScoreData instance with null values.
        /// </summary>
        /// <returns>The loaded high score data, or a new instance if loading fails.</returns>
        private HighScoreData LoadHighScores()
        {
            try
            {
                if (!File.Exists(filePath))
                    return new HighScoreData();

                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<HighScoreData>(json) ?? new HighScoreData();
            }
            catch
            {
                return new HighScoreData();
            }
        }

        /// <summary>
        /// Saves the high scores to the JSON file.
        /// </summary>
        private void SaveHighScores()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(highScores, options);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Represents the structure of the high score data stored in the JSON file, with properties for each difficulty level. Each property is nullable to indicate that no score has been recorded yet.
        /// </summary>
        private class HighScoreData
        {
            [JsonPropertyName("easy")]
            public int? Easy { get; set; }

            [JsonPropertyName("medium")]
            public int? Medium { get; set; }

            [JsonPropertyName("hard")]
            public int? Hard { get; set; }
        }
    }
}
