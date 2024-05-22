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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            frame1.NavigationService.Navigate(new Pages.ClientTable());
            Reset(bt1);
        }

        public void Reset(Border bt)
        {
            SolidColorBrush gray = this.Resources["grey"] as SolidColorBrush;
            bt1.Background = gray; 
            bt2.Background = gray;
            bt3.Background = gray;
            bt4.Background = gray;
            bt5.Background = gray;
            bt.Background = this.Resources["blue"] as SolidColorBrush;
        }
        /// <summary>
        /// Таблица Клиенты
        /// </summary>
        private void ClientTable_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.ClientTable());
            Reset(bt1);
        }

        /// <summary>
        /// Таблица Фотографы
        /// </summary>
        private void PhotographersTable_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.PhotographerTable());
            Reset(bt3);
        }

        /// <summary>
        /// Таблица Прайс-листа
        /// </summary>
        private void UslugiTable_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.UslugiTable());
            Reset(bt2);
        }

        /// <summary>
        /// Таблица локаций
        /// </summary>
        private void LocationsTable_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.LocationsTable());
            Reset(bt5);
        }

        /// <summary>
        /// Таблица договоров
        /// </summary>
        private void DogovorTable_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.DogovorTable());
            Reset(bt4);
        }
    }
}
