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
    /// Логика взаимодействия для PhotographerTable.xaml
    /// </summary>
    public partial class PhotographerTable : Page
    {
        public PhotographerTable()
        {
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// Редактирование фотографа
        /// </summary>
        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Фотограф;
            AddPhotographerWin edit = new AddPhotographerWin(selected);
            edit.ShowDialog();
            currentList = MainWindow.db.Фотограф.ToList();
            Refresh();
        }

        /// <summary>
        /// Удаление фотографа
        /// </summary>
        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var editButton = sender as Button;
                var deleted = editButton.DataContext as Фотограф;

                if (deleted != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Фотограф.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        currentList = MainWindow.db.Фотограф.ToList();
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
        /// Добавление фотографа
        /// </summary>
        private void PhotogrAdd_Click(object sender, RoutedEventArgs e)
        {
            Фотограф photog = new Фотограф();
            photog.Табельный_номер = 0;
            AddPhotographerWin edit = new AddPhotographerWin(photog);
            edit.ShowDialog();

            currentList = MainWindow.db.Фотограф.ToList();
            Refresh();
        }

        /// <summary>
        /// Просмотр портфолио фотографа
        /// </summary>
        private void bt_Portfolio(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Фотограф;
            PortfolioWin win = new PortfolioWin(selected);
            win.ShowDialog();
        }

        IEnumerable<Фотограф> currentList = MainWindow.db.Фотограф.ToList();

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
            tbSearch.Text = "";
            currentList = MainWindow.db.Фотограф.ToList();
            Refresh();
        }

        /// <summary>
        /// Поиск
        /// </summary>
        private void searchTextChanged(object sender, TextChangedEventArgs e)
        {
            string seachText = tbSearch.Text;
            var newlist = currentList;
            if (!string.IsNullOrWhiteSpace(seachText))
            {
                newlist = currentList.Where(p => p.Фамилия.ToLower().StartsWith(seachText.ToLower())).ToList();
            }

            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = newlist.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            dg1.ItemsSource = listPage;
        }

        /// <summary>
        /// Показ группы фильтров
        /// </summary>
        private void bt_Filter(object sender, RoutedEventArgs e)
        {
            if (spFilter.Visibility != Visibility.Visible)
                spFilter.Visibility = Visibility.Visible;
            else
                spFilter.Visibility = Visibility.Hidden;
        }

        private int _currentPage = 1;
        private int _count = 8;
        private int _maxPages;

        /// <summary>
        /// Обнолвение списка
        /// </summary>
        private void Refresh()
        {
            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = currentList.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            dg1.ItemsSource = listPage;
        }

        private void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            Refresh();
        }

        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 1) _currentPage = 1;
            else
                _currentPage--;
            Refresh();
        }

        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _maxPages) _currentPage = _maxPages;
            else
                _currentPage++;
            Refresh();
        }

        private void GoToLastPage(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPages;
            Refresh();
        }
    }
}
