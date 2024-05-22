using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddDogovorWin.xaml
    /// </summary>
    public partial class AddDogovorWin : Window
    {
        private Договор dogovor;
        public static bool IsForClient = false;
        public AddDogovorWin(Договор dogovor)
        {
            InitializeComponent();

            this.dogovor = dogovor;
            DataContext = dogovor;
            cb_Clients.ItemsSource = MainWindow.db.Клиент.ToList();
            cb_Location.ItemsSource = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да" || p.Номер_локации == dogovor.Номер_локации);
            
            cb_Status.ItemsSource = MainWindow.db.Статус.ToList();
            cb_Usluga.ItemsSource = MainWindow.db.Вид_услуги.ToList();
            cb_Photog.ItemsSource = MainWindow.db.Фотограф.ToList();
            if (dogovor.Локация != null)
            {
                if (dogovor.Локация.Во_владении_компании.Trim() == "нет")
                {
                    rb2.IsChecked = true;
                    cb_Location.SelectedItem = dogovor.Локация;
                }
                else
                {
                    rb1.IsChecked = true;
                }
            }
            else
            {
                rb1.IsChecked = true;
            }

            if(IsForClient)
            {
                cb_Clients.IsEnabled = false;
                cb_Status.IsEnabled = false;
            }
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cb_Status.Text) || string.IsNullOrEmpty(cb_Usluga.Text) || string.IsNullOrEmpty(tbDateEnd.Text) || string.IsNullOrEmpty(tbDateStart.Text))
                    throw new Exception("Заполните все поля");

                DateTime dateStart, dateEnd;
                var cultureInfo = new CultureInfo("en-US");
                dateEnd = DateTime.ParseExact(tbDateEnd.Text.Trim(), "G", cultureInfo);
                dateStart = DateTime.ParseExact(tbDateStart.Text.Trim(), "G", cultureInfo);
                var photog = cb_Photog.SelectedItem as Фотограф;

                var list = MainWindow.db.Договор.Where(x=> x.Номер_фотографа == photog.Табельный_номер && 
                ((dateStart <= x.Окончание_съемки && dateStart >= x.Начало_съемки) || (dateEnd >= x.Начало_съемки && dateEnd <= x.Окончание_съемки)) 
                && x.Номер_договора != dogovor.Номер_договора);

                if ((dateEnd - dateStart).TotalHours > 12)
                    throw new Exception("У фотографа максимум 12-ми часовой график");

                if (dateEnd.TimeOfDay > new TimeSpan(20, 00, 00) || dateStart.TimeOfDay < new TimeSpan(08, 00, 00))
                    throw new Exception("Фотограф работает с 08:00 до 20:00");

                if (list.Count() > 0)
                    throw new Exception("Фотограф занят.");
                
                    

                if (rb2.IsChecked == true)
                {
                    Локация loc = new Локация { Номер_локации = 0, Адрес = newAddress.Text, Во_владении_компании = "нет" };
                    MainWindow.db.Локация.Add(loc);
                    MainWindow.db.SaveChanges();
                    dogovor.Локация = loc;
                }
                else
                {
                    dogovor.Локация = cb_Location.SelectedItem as Локация;
                }

                if (dogovor.Номер_договора == 0)
                {
                    MainWindow.db.Договор.Add(dogovor);
                }

                MainWindow.db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            cb_Location.Visibility = Visibility.Visible;
            newAddress.Visibility = Visibility.Collapsed;
            cb_Location.ItemsSource = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да" || p.Номер_локации == dogovor.Номер_локации);
            if(dogovor.Локация != null)
                cb_Location.SelectedItem = dogovor.Локация;
        }

        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            cb_Location.Visibility = Visibility.Collapsed;
            newAddress.Visibility = Visibility.Visible;
            if(dogovor.Локация != null)
            {
                if (dogovor.Локация.Во_владении_компании.Trim() == "нет")
                {
                    newAddress.Text = dogovor.Локация.Адрес;
                }
                else
                {
                    newAddress.Text = "";
                }
            }
            
            
        }
    }
}
