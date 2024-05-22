using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddVidUslugiWin.xaml
    /// </summary>
    public partial class AddVidUslugiWin : Window
    {
        private Вид_услуги vid;
        public AddVidUslugiWin(Вид_услуги vid)
        {
            InitializeComponent();
            this.vid = vid;
            DataContext = vid;
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbDesc.Text) || string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbCost.Text))
                    throw new Exception("Заполните все поля!");

                double cost = 0;

                if (!double.TryParse(tbCost.Text, out cost))
                    throw new Exception("Неверная стоимость");

                if (vid.Номер_вида == 0)
                {
                    MainWindow.db.Вид_услуги.Add(vid);
                }

                MainWindow.db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
