using System.Windows;

namespace HangManV01.Views
{
    /// <summary>
    /// Interaction logic for Rules.xaml
    /// </summary>
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