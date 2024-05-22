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
    /// Логика взаимодействия для ClientTable.xaml
    /// </summary>
    public partial class ClientTable : Page
    {
        IEnumerable<Клиент> currentList = MainWindow.db.Клиент.ToList();

        public ClientTable()
        {
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        private void ClientAdd_Click(object sender, RoutedEventArgs e)
        {
            Клиент client = new Клиент();
            client.Номер_клиента = 0;
            AddClientWin edit = new AddClientWin(client);
            edit.ShowDialog();
            currentList = MainWindow.db.Клиент.ToList();
            Refresh();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        private void ClientDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deletedClient = dg1.SelectedItem as Клиент;

                if (deletedClient != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Клиент.Remove(deletedClient);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        currentList = MainWindow.db.Клиент.ToList();
                        Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование клиента
        /// </summary>
        private void ClientEdit_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selectedClient = editButton.DataContext as Клиент;
            AddClientWin edit = new AddClientWin(selectedClient);
            edit.ShowDialog();
            currentList = MainWindow.db.Клиент.ToList();
            Refresh();
        }

        /// <summary>
        /// Показ окна фильтров
        /// </summary>
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if (spFilter.Visibility != Visibility.Visible)
                spFilter.Visibility = Visibility.Visible;
            else
                spFilter.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Применение фильтров
        /// </summary>
        private void btFilterAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime from, to;

                if (!DateTime.TryParse(dateFrom.Text, out from) || !DateTime.TryParse(dateTo.Text, out to))
                    throw new Exception("Введите верный формат даты");

                currentList = currentList.Where(p => p.Дата_рождения <= Convert.ToDateTime(dateTo.Text) && p.Дата_рождения >= Convert.ToDateTime(dateFrom.Text));

                Refresh();
                spFilter.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Сброс фильтров
        /// </summary>
        private void btFilterReset_Click(object sender, RoutedEventArgs e)
        {
            dateFrom.Text = "";
            dateTo.Text = "";
            tbFamilia.Text = "";

            dg1.ItemsSource = null;
            currentList = MainWindow.db.Клиент.ToList();
            Refresh();
        }

        /// <summary>
        /// Поиск
        /// </summary>
        private void searchTextChanged(object sender, TextChangedEventArgs e)
        {
            string seachText = tbFamilia.Text;
            var newlist = currentList;
            if (!string.IsNullOrWhiteSpace(seachText))
            {
                newlist = currentList.Where(p => p.Фамилия.ToLower().Contains(seachText.ToLower()) || p.Имя.ToLower().Contains(seachText)).ToList();
            }

            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = newlist.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            dg1.ItemsSource = listPage;

        }

        private int _currentPage = 1;
        private int _count = 9;
        private int _maxPages;

        /// <summary>
        /// Обновление списка клиентов
        /// </summary>
        private void Refresh()
        {
            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = currentList.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            dg1.ItemsSource = listPage;
        }

        /// <summary>
        /// Переход на 1-ую страницу
        /// </summary>
        private void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            Refresh();
        }

        /// <summary>
        /// Предыдущая страница
        /// </summary>
        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 1) _currentPage = 1;
            else
                _currentPage--;
            Refresh();
        }

        /// <summary>
        /// След. страница
        /// </summary>
        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _maxPages) _currentPage = _maxPages;
            else
                _currentPage++;
            Refresh();
        }

        /// <summary>
        /// Последняя страница
        /// </summary>
        private void GoToLastPage(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPages;
            Refresh();
        }
    }
}
