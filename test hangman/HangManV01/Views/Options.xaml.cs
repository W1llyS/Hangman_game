using System;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using HangManV01.Services;

namespace HangManV01.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private readonly IExcelService _excelService;

        public Options()
        {
            InitializeComponent();
            _excelService = new ExcelService();
            LoadWordsIntoTextBox();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Word.Text))
            {
                MessageBox.Show("Please enter a word in the 'Word' field.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Hint.Text))
            {
                MessageBox.Show("Please enter a hint in the 'Hint' field.");
                return;
            }

            try
            {
                _excelService.AddWord(Word.Text, Hint.Text);
                LoadWordsIntoTextBox();

                // Clear the input fields after successful addition
                Word.Text = string.Empty;
                Hint.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Delete.Text))
            {
                MessageBox.Show("Please enter a word to delete.");
                return;
            }

            try
            {
                _excelService.DeleteWord(Delete.Text);
                LoadWordsIntoTextBox();

                // Clear the delete field after successful deletion
                Delete.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void LoadWordsIntoTextBox()
        {
            try
            {
                var words = _excelService.GetAllWords();
                StringBuilder sb = new StringBuilder();

                foreach (var word in words)
                {
                    sb.AppendLine(word);
                }

                TextBoxWordsPreview.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading words: " + ex.Message);
            }
        }

        private void Delete_GotFocus(object sender, RoutedEventArgs e)
        {
            Delete.Text = string.Empty;
        }

        private void Word_GotFocus(object sender, RoutedEventArgs e)
        {
            Word.Text = string.Empty;
        }

        private void Hint_GotFocus(object sender, RoutedEventArgs e)
        {
            Hint.Text = string.Empty;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) =>
            NavigationHelper.BackToMain(this);
    }
}