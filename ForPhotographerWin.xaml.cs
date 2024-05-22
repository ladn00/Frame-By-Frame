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
    /// Логика взаимодействия для ForPhotographerWin.xaml
    /// </summary>
    public partial class ForPhotographerWin : Window
    {
        public ForPhotographerWin()
        {
            InitializeComponent();
            rb1Menu.IsChecked = true;
        }

        private void rbHomeChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.HomePage());
        }

        private void rbUchetChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.DogovorsForPhotog(MainWindow.curPhotog));
        }


        private void Out_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();

        }

        private void rbPortfolioChecked(object sender, RoutedEventArgs e)
        {
            PortfolioWin win = new PortfolioWin(MainWindow.curPhotog);
            win.ShowDialog();
            rb4Menu.IsChecked = false;
        }

        private void rbScheduleChecked(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new Pages.SchedulePhotog(MainWindow.curPhotog));
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                System.Diagnostics.Process.Start("Frame By Frame Руководство пользователя.chm");
        }
    }
}
