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


        /// <summary>
        /// Every new game starts with an empty list of guessed letters, no failed attempts, and a default max attempts
        /// </summary>
        public WordModel()
        {
            GuessedLetters = new List<char>(); // start empty
            FailedAttempts = 0;                // nothing guessed wrong yet
            MaxAttempts = 5;                   // default max attempts
            IsGameWon = false;
            IsGameLost = false;
        }
    }
}