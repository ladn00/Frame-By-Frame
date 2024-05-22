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
    /// Логика взаимодействия для ForClientWindow.xaml
    /// </summary>
    public partial class ForClientWindow : Window
    {
        public ForClientWindow()
        {
            InitializeComponent();
            rb1Menu.IsChecked = true;
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void rbHomeChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.HomePage());
        }

        private void rbZakazyChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.DogovorsForClient());
        }

        private void rbLocationsChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.LocationsForClient());
        }

        private void rbPhotogChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.PhotogsForClient());
        }

        private void rbVidUlsugChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.VidUslugiForClients());
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                System.Diagnostics.Process.Start("Frame By Frame Руководство пользователя.chm");
        }
    }
}
