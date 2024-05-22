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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для SchedulePhotog.xaml
    /// </summary>
    public partial class SchedulePhotog : Page
    {
        Фотограф photog;
        List<Договор> list = MainWindow.db.Договор.Where(p => p.Номер_фотографа == MainWindow.curPhotog.Табельный_номер).ToList();
        public static List<DateTime> dates = new List<DateTime>();
        
        public SchedulePhotog(Фотограф photog)
        {
            InitializeComponent();
            this.photog = photog;
            dg1.ItemsSource = null;
            
            foreach (var d in list)
            {
                if (d.Начало_съемки.Month == calendar1.DisplayDate.Month &&
                    !dates.Contains(d.Начало_съемки.Date))
                {
                    dates.Add(d.Начало_съемки);
                    calendar1.SelectedDates.Add(d.Начало_съемки);
                }

            }
        }

        private void dates_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DateTime> selDates = new List<DateTime>();
            foreach (DateTime d in calendar1.SelectedDates)
                selDates.Add(DateTime.Parse(d.ToString("d")));

            List<Договор> list = new List<Договор>();
            foreach(var d in photog.Договор.ToList())
            {
                foreach(var c in selDates)
                {
                    if (c == DateTime.Parse(d.Начало_съемки.ToString("d")) && !list.Contains(d))
                    {
                        list.Add(d);
                    }
                }
                
            }
            dg1.ItemsSource = null;
            dg1.ItemsSource = list;

        }

        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Договор;
            AddDogovorWin edit = new AddDogovorWin(selected);
            edit.cb_Photog.IsEnabled = false;
            edit.cb_Clients.IsEnabled = false;
            edit.ShowDialog();
        }
        
        private void calendar_DisplayChanged(object sender, CalendarDateChangedEventArgs e)
        {
            dg1.ItemsSource = null;
            calendar1.SelectedDates.Clear();
            foreach (var d in list)
            {
                if (d.Начало_съемки.Month == calendar1.DisplayDate.Month &&
                    !dates.Contains(d.Начало_съемки.Date))
                {
                    dates.Add(d.Начало_съемки);
                    calendar1.SelectedDates.Add(d.Начало_съемки);

                }

            }
        }
        
    }
    
}
