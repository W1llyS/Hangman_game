using HangManV01.Commands;
using HangManV01.Models;
using HangManV01.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HangManV01.Views;
using System.Collections.ObjectModel;

namespace HangManV01.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IExcelService _excelService;
        private WordModel _currentGame;
        private string _displayWord;
        private string _hint;
        private BitmapImage _hangmanImage;
        private bool _isGameOver;
        private ObservableCollection<char> _guessedLetters;

        public MainWindowViewModel()
        {
            _excelService = new ExcelService();
            _guessedLetters = new ObservableCollection<char>();

            // Initialize commands
            LetterGuessCommand = new RelayCommand(GuessLetter, CanGuessLetter);
            BackCommand = new RelayCommand(GoBack);

            // Start new game
            StartNewGame();
        }

        public string DisplayWord
        {
            get => _displayWord;
            set => SetProperty(ref _displayWord, value);
        }

        public string Hint
        {
            get => _hint;
            set => SetProperty(ref _hint, value);
        }

        public BitmapImage HangmanImage
        {
            get => _hangmanImage;
            set => SetProperty(ref _hangmanImage, value);
        }

        public bool IsGameOver
        {
            get => _isGameOver;
            set => SetProperty(ref _isGameOver, value);
        }

        public ObservableCollection<char> GuessedLetters
        {
            get => _guessedLetters;
            set => SetProperty(ref _guessedLetters, value);
        }

        public ICommand LetterGuessCommand { get; }
        public ICommand BackCommand { get; }

        private void StartNewGame()
        {
            _currentGame = _excelService.GetRandomWord();
            DisplayWord = _currentGame.DisplayWord;
            Hint = _currentGame.Hint;
            IsGameOver = false;
            HangmanImage = null;
            GuessedLetters.Clear(); // Clear guessed letters for new game
        }

        private bool CanGuessLetter(object parameter)
        {
            if (IsGameOver || !(parameter is string letter) || string.IsNullOrEmpty(letter))
                return false;

            char letterChar = letter.ToUpper()[0];
            return !_currentGame.GuessedLetters.Contains(letterChar);
        }

        private void GuessLetter(object parameter)
        {
            if (!(parameter is string selectedLetter) || IsGameOver)
                return;

            char letter = selectedLetter.ToUpper()[0];

            // Check if letter already guessed
            if (_currentGame.GuessedLetters.Contains(letter))
                return;

            _currentGame.GuessedLetters.Add(letter);
            GuessedLetters.Add(letter); // Add to observable collection for UI binding

            if (_currentGame.Word.ToUpper().Contains(letter))
            {
                // Correct guess - reveal letters
                RevealLetter(letter);

                // Check if game is won
                if (!DisplayWord.Contains('_'))
                {
                    _currentGame.IsGameWon = true;
                    IsGameOver = true;
                    MessageBox.Show("CONGRATULATION YOU WON!", "Victory", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Wrong guess
                _currentGame.FailedAttempts++;

                if (_currentGame.FailedAttempts <= _currentGame.MaxAttempts)
                {
                    UpdateHangmanImage(_currentGame.FailedAttempts);
                }

                if (_currentGame.FailedAttempts >= _currentGame.MaxAttempts)
                {
                    _currentGame.IsGameLost = true;
                    IsGameOver = true;
                    MessageBox.Show($"YOU LOST! CORRECT WORD WAS: {_currentGame.Word}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void RevealLetter(char selectedLetter)
        {
            var wordArray = DisplayWord.Split(' ');
            var currentWord = _currentGame.Word.ToUpper();

            for (int i = 0; i < currentWord.Length; i++)
            {
                if (currentWord[i] == selectedLetter)
                {
                    wordArray[i] = selectedLetter.ToString();
                }
            }

            DisplayWord = string.Join(" ", wordArray);
        }

        private void UpdateHangmanImage(int attemptNumber)
        {
            try
            {
                string imagePath = Path.Combine(Environment.CurrentDirectory, "Images", $"hangman{attemptNumber}.bmp");
                if (File.Exists(imagePath))
                {
                    HangmanImage = new BitmapImage(new Uri(imagePath));
                }
            }
            catch (Exception ex)
            {
                // Handle image loading error
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
        }

        private void GoBack(object parameter)
        {
            var menuWindow = new MainMenu();
            menuWindow.Show();
        }

        // Method to check if a specific letter has been guessed (for XAML binding)
        public bool IsLetterGuessed(char letter)
        {
            return _currentGame?.GuessedLetters.Contains(char.ToUpper(letter)) ?? false;
        }
    }
}