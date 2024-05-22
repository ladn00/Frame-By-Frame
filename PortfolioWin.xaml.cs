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

namespace Фотостудия
{
    /// <summary>
    /// Логика взаимодействия для PortfolioWin.xaml
    /// </summary>
    public partial class PortfolioWin : Window
    {
        private Фотограф photog;

        public PortfolioWin(Фотограф photog)
        {
            InitializeComponent();
            this.photog = photog;
            DataContext = photog;
            lw1.ItemsSource = photog.Портфолио_фотографа.ToList();
        }

        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Портфолио_фотографа;
            AddPortfolio edit = new AddPortfolio(photog, selected);
            edit.ShowDialog();
            lw1.ItemsSource = photog.Портфолио_фотографа.ToList();
        }

        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = lw1.SelectedItem as Портфолио_фотографа;

                if (deleted != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Портфолио_фотографа.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        lw1.ItemsSource = photog.Портфолио_фотографа.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bt_Add(object sender, RoutedEventArgs e)
        {
            Портфолио_фотографа jour = new Портфолио_фотографа();
            jour.Номер_портфолио = 0;
            AddPortfolio edit = new AddPortfolio(photog, jour);
            edit.ShowDialog();
            lw1.ItemsSource = photog.Портфолио_фотографа.ToList();
        }
    }
}
