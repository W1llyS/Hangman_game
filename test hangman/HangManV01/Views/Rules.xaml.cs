using System.Windows;

namespace HangManV01.Views
{
    public partial class Rules : Window
    {
        public Rules()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e) =>
        NavigationHelper.BackToMain(this);


    }
}