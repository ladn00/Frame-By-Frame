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
    /// Логика взаимодействия для DogovorsForClient.xaml
    /// </summary>
    public partial class DogovorsForClient : Page
    {
        public DogovorsForClient()
        {
            InitializeComponent();
            dg1.ItemsSource = MainWindow.db.Договор.Where(p=>p.Номер_клиента == MainWindow.curClient.Номер_клиента).ToList();
        }

        /// <summary>
        /// Отменение заказа клиентом
        /// </summary>
        private void CancelDogovor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = dg1.SelectedItem as Договор;

                if (selected != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите отменить заказ?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        if (selected.Начало_съемки < DateTime.Now - TimeSpan.FromDays(1))
                            throw new Exception("Нельзя отменить заказ позже, чем за сутки до него");

                        selected.Статус_договора = MainWindow.db.Статус.ToList()[2].Номер_статуса;
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Договор отменен!");
                        dg1.ItemsSource = MainWindow.db.Договор.Where(p => p.Номер_клиента == MainWindow.curClient.Номер_клиента).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Договор selected = new Договор();

        /// <summary>
        /// Показ журнала съемки
        /// </summary>
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = dg1.SelectedItem as Договор;
            frame1.NavigationService.Navigate(new JournalForClient(selected));
        }

        /// <summary>
        /// Добавление договора
        /// </summary>
        private void newDogovor_Click(object sender, RoutedEventArgs e)
        {
            Договор dog = new Договор();
            dog.Номер_договора = 0;
            dog.Номер_клиента = MainWindow.curClient.Номер_клиента;
            dog.Статус_договора = 2;
            AddDogovorWin.IsForClient = true;
            AddDogovorWin win = new AddDogovorWin(dog);
            win.ShowDialog();
            dg1.ItemsSource = MainWindow.db.Договор.Where(p => p.Номер_клиента == MainWindow.curClient.Номер_клиента).ToList();
        }
    }
}
