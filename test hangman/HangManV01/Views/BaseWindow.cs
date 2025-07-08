using System.Windows;
using System.Windows.Controls;

namespace HangManV01.Views
{
    public class BaseWindow : Window
    {
        protected void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (Owner is MainMenu menu) menu.Show();
        }
    }
}
