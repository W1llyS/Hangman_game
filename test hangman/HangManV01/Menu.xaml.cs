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

            // Pokud chcete zavřít aktuální okno po otevření hry, můžete přidat následující řádek:
            this.Close();
        }

        private void OptionsItem_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();
            optionsWindow.Owner = this;  // Nastavte okno Menu jako vlastníka okna Options
            optionsWindow.Show();

            // Skrývání aktuálního okna místo zavření
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
