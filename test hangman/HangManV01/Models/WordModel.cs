using System.Collections.Generic;

namespace HangManV01.Models
{
    public class WordModel
    {
        public string Word { get; set; }
        public string Hint { get; set; }
        public string DisplayWord { get; set; }
        public List<char> GuessedLetters { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsGameWon { get; set; }
        public bool IsGameLost { get; set; }
        public int MaxAttempts { get; set; }

        public WordModel()
        {
            GuessedLetters = new List<char>(); // start empty
            FailedAttempts = 0;                // nothing guessed wrong yet
            MaxAttempts = 5;                   // default max attempts -- can be adjusted later
            IsGameWon = false;
            IsGameLost = false;
        }
    }
}