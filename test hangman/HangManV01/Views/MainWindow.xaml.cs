using System.Windows;

namespace HangManV01.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) =>
        NavigationHelper.BackToMain(this);

    }
}
