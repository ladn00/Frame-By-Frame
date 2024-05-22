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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для PhotogsForClient.xaml
    /// </summary>
    public partial class PhotogsForClient : Page
    {
        public PhotogsForClient()
        {
            InitializeComponent();
            dg1.ItemsSource = MainWindow.db.Фотограф.ToList();
        }

        private void bt_Portfolio(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Фотограф;
            PortfolioForClient win = new PortfolioForClient(selected);
            win.ShowDialog();
        }
    }
}
