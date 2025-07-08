using HangManV01.Views;
using System.Windows;

public static class NavigationHelper
{
    public static void BackToMain(Window current)
    {
        var menu = new MainMenu();
        Application.Current.MainWindow = menu;   // hand over “main window” role - so the app won't close
        menu.Show();
        current.Close();
    }
}
