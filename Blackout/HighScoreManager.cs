using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackout
{
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public static class GameDifficultyExtensions
    {
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

        private void SaveHighScores()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(highScores, options);
            File.WriteAllText(filePath, json);
        }

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
