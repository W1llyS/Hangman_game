using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HangManV01
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
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
            optionsWindow.Owner = this;  // Sets the Menu window as the owner of the Options window.
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
