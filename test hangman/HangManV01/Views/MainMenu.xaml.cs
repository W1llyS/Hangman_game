using System.Windows;

namespace HangManV01.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow();
            gameWindow.Show();
            this.Close();
        }

        private void OptionsItem_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();
            optionsWindow.Owner = this;
            optionsWindow.Show();
            this.Hide();
        }

        private void RulesItem_Click(object sender, RoutedEventArgs e)
        {
            Rules rulesWindow = new Rules();
            rulesWindow.Show();
            this.Close();
        }
    }
}