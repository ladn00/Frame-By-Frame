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
    /// Логика взаимодействия для PortfolioForClient.xaml
    /// </summary>
    public partial class PortfolioForClient : Window
    {
        Фотограф photog;
        public PortfolioForClient(Фотограф photog)
        {
            InitializeComponent();
            InitializeComponent();
            this.photog = photog;
            lw1.ItemsSource = photog.Портфолио_фотографа.ToList();
        }
    }
}
