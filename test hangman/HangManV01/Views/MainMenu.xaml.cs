using System.Windows;

namespace HangManV01.Views
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var w = new MainWindow();
            App.Current.MainWindow = w;
            w.Show();
            this.Close();
        }

        private void OptionsItem_Click(object sender, RoutedEventArgs e)
        {
            
            var w = new Options();
            App.Current.MainWindow = w;
            w.Show();
            this.Close();
        }

        private void RulesItem_Click(object s, RoutedEventArgs e)
        {
            var w = new Rules();
            Application.Current.MainWindow = w;   // hand off “main window” role
            w.Show();
            this.Close();                         
        }
    }
}