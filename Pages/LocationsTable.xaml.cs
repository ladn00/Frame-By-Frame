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
    /// Логика взаимодействия для LocationsTable.xaml
    /// </summary>
    public partial class LocationsTable : Page
    {
        IEnumerable<Локация> currentList = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да");

        public LocationsTable()
        {
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// Добавление локации
        /// </summary>
        private void LocationAdd_Click(object sender, RoutedEventArgs e)
        {
            Локация location = new Локация();
            location.Номер_локации = 0;
            AddLocation edit = new AddLocation(location);
            edit.ShowDialog();
            currentList = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да");
            Refresh();
        }

        /// <summary>
        /// Редактирование локации
        /// </summary>
        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Локация;
            AddLocation edit = new AddLocation(selected);
            edit.ShowDialog();
            currentList = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да");
            Refresh();
        }

        /// <summary>
        /// Удаление локации
        /// </summary>
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = lw1.SelectedItem as Локация;

                if (deleted != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Локация.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        currentList = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да");
                        Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int _currentPage = 1;
        private int _count = 2;
        private int _maxPages;

        /// <summary>
        /// Обновление списка
        /// </summary>
        private void Refresh()
        {
            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = currentList.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            lw1.ItemsSource = listPage;
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

        /// <summary>
        /// Сброс фильтров
        /// </summary>
        private void btFilterReset_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            currentList = MainWindow.db.Локация.ToList().Where(p => p.Во_владении_компании.Trim() == "да");
            Refresh();
        }

        /// <summary>
        /// Применение фильтров
        /// </summary>
        private void btFilterAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Refresh();
                spFilter.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                newlist = currentList.Where(p => p.Адрес.ToLower().Contains(seachText.ToLower())).ToList();
            }

            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = newlist.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            lw1.ItemsSource = listPage;
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
    }
}
