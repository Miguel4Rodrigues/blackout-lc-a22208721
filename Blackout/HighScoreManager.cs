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

