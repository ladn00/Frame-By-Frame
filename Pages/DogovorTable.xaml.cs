using ScottPlot.Colormaps;
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
using word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Xml;
using System.Globalization;

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для DogovorTable.xaml
    /// </summary>
    public partial class DogovorTable : Page
    {
        IEnumerable<Договор> currentList = MainWindow.db.Договор.ToList();
        private int _currentPage = 1;
        private int _count = 5;
        private int _maxPages;

        public DogovorTable()
        {
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// Редактирование договора
        /// </summary>
        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Договор;
            AddDogovorWin edit = new AddDogovorWin(selected);
            edit.ShowDialog();
            currentList = MainWindow.db.Договор.ToList();
            Refresh();
        }

        /// <summary>
        /// Удаление договора
        /// </summary>
        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = dg1.SelectedItem as Договор;

                if (deleted != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Договор.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        currentList = MainWindow.db.Договор.ToList();
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
        /// Добавление договора
        /// </summary>
        private void DogovorAdd_Click(object sender, RoutedEventArgs e)
        {
            Договор dog = new Договор();
            dog.Номер_договора = 0;
            AddDogovorWin edit = new AddDogovorWin(dog);
            edit.ShowDialog();
            currentList = MainWindow.db.Договор.ToList();
            Refresh();
        }

        /// <summary>
        /// Применение фильтров
        /// </summary>
        private void btFilterAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newList = MainWindow.db.Договор.ToList();
                DateTime from, to;

                if (!String.IsNullOrEmpty(dateFrom.Text) && (!DateTime.TryParse(dateFrom.Text, out from) || !DateTime.TryParse(dateTo.Text, out to)))
                    throw new Exception("Введите верный формат даты");

                List<int> stat = new List<int>();

                //Статус договора
                if (cbDone.IsChecked == true)
                    stat.Add(1);

                if (cbWaiting.IsChecked == true)
                    stat.Add(2);

                if (cbCanceled.IsChecked == true)
                    stat.Add(3);

                if (!String.IsNullOrEmpty(dateFrom.Text))
                    currentList = newList.Where(p => p.Окончание_съемки <= Convert.ToDateTime(dateTo.Text) && p.Начало_съемки >= Convert.ToDateTime(dateFrom.Text) && stat.Contains(p.Статус_договора));
                else
                    currentList = newList.Where(p => stat.Contains(p.Статус_договора));

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
                newlist = currentList.Where(p => p.Клиент.FIO.ToLower().Contains(seachText.ToLower()) || p.Фотограф.FIO.ToLower().Contains(seachText.ToLower())).ToList();
            }

            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var listPage = newlist.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            dg1.ItemsSource = listPage;
        }

        /// <summary>
        /// Обновление
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
        /// Сброс фильтров
        /// </summary>
        private void btFilterReset_Click(object sender, RoutedEventArgs e)
        {
            cbCanceled.IsChecked = true;
            cbDone.IsChecked = true;
            cbWaiting.IsChecked = true;
            dateFrom.Text = "";
            dateTo.Text = "";
            tbSearch.Text = "";
            currentList = MainWindow.db.Договор.ToList();
            Refresh();
        }

        /// <summary>
        /// Показ окна фильтров
        /// </summary>
        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            if (spFilter.Visibility != Visibility.Visible)
                spFilter.Visibility = Visibility.Visible;
            else
                spFilter.Visibility = Visibility.Hidden;
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

        Договор selected = new Договор();

        /// <summary>
        /// Показ журнала фотосъемки
        /// </summary>
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = dg1.SelectedItem as Договор;
            frame1.NavigationService.Navigate(new Pages.JournalTable(selected));
        }

        /// <summary>
        /// Печать договора
        /// </summary>
        private void btPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                word.Application app = new word.Application();
                var selected = dg1.SelectedItem as Договор;
                if (selected == null)
                    throw new Exception("Выберите договор");

                string filename = AppDomain.CurrentDomain.BaseDirectory + "Договор.docx";
                string fileTitle = "Договор " + selected.Номер_договора.ToString();
                string path = AppDomain.CurrentDomain.BaseDirectory + fileTitle;
                File.Copy(filename, path, true);

                word.Document doc = app.Documents.Open(path);

                FindAndReplace(app, @"номерДоговора", selected.Номер_договора.ToString());
                FindAndReplace(app, @"числоТек", DateTime.Now.Day.ToString());
                FindAndReplace(app, @"месяцТек", DateTime.Now.ToString("MMMM"));
                FindAndReplace(app, @"годТек", DateTime.Now.Year.ToString());
                FindAndReplace(app, @"ФИОФотографа", selected.Фотограф.Фамилия.Trim() + " " + selected.Фотограф.Имя.Trim() + " " + selected.Фотограф.Отчество.Trim());
                FindAndReplace(app, @"датаРождения", selected.Фотограф.Дата_рождения.ToString("d") + "г.");
                FindAndReplace(app, @"числоЗаказа", selected.Начало_съемки.Day.ToString());
                FindAndReplace(app, @"месяцЗаказа", selected.Начало_съемки.ToString("MMMM"));
                FindAndReplace(app, @"годЗаказа", selected.Начало_съемки.Year.ToString());
                FindAndReplace(app, @"стоимостьСъемки", selected.Стоимость.ToString("f2") + " руб.");
                FindAndReplace(app, @"инициалыФотографа", selected.Фотограф.FIO);
                FindAndReplace(app, @"инициалыКлиента", selected.Клиент.FIO);
                FindAndReplace(app, @"телефонФотографа", selected.Фотограф.Телефон);
                FindAndReplace(app, @"телефонКлиента", selected.Клиент.Телефон);
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// Поиск и замена
        /// </summary>
        private void FindAndReplace(word.Application app, string f, string r)
        {
            app.Selection.Find.ClearFormatting();
            word.Range range = app.Selection.Range;
            app.Selection.Find.Replacement.ClearFormatting();
            app.Selection.Find.Replacement.Text = r;
            app.Selection.Find.Execute(f, Replace: word.WdReplace.wdReplaceAll);

            app.Selection.WholeStory();
            app.Options.DefaultHighlightColorIndex = word.WdColorIndex.wdNoHighlight;
            app.Selection.Range.HighlightColorIndex = word.WdColorIndex.wdWhite;
        }


        /// <summary>
        /// Экспорт договоров в xml-файл
        /// </summary>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            List<Договор> dogovorsToExport = new List<Договор>();

            if (dg1.SelectedItems.Count < 1)
            {
                MessageBox.Show("Вы не выбрали записи для экспорта.");
                return;
            }

            foreach (Договор el in dg1.SelectedItems)
            {
                dogovorsToExport.Add(el);
            }

            using (XmlWriter writer = XmlWriter.Create("..//..//export/output.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("dogovors");

                foreach (var el in dogovorsToExport)
                {
                    writer.WriteStartElement("product");
                    writer.WriteElementString("clientid", el.Номер_клиента.ToString());
                    writer.WriteElementString("viduslugiid", el.Вид_услуги.ToString());
                    writer.WriteElementString("photogid", el.Номер_фотографа.ToString());
                    writer.WriteElementString("locationid", el.Номер_локации.ToString());
                    writer.WriteElementString("datestart", el.Начало_съемки.ToString(new CultureInfo("en-us")));
                    writer.WriteElementString("dateend", el.Окончание_съемки.ToString(new CultureInfo("en-us")));
                    writer.WriteElementString("statusid", el.Статус_договора.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();

                MessageBox.Show("Файл \"output.xml\" был сохранен в папке \"export\".");
            }
        }

        private void XmlImport_Click(object sender, RoutedEventArgs e)
        {
            string filename = "";
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files (*.xml)|*.xml";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;

                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        var importedDogovor = new Договор();
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "clientid")
                        {
                            importedDogovor.Номер_клиента = int.Parse(reader.ReadElementContentAsString());
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "viduslugiid")
                        {
                            importedDogovor.Вид_услуги = int.Parse(reader.ReadElementContentAsString());
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "photogid")
                        {
                            importedDogovor.Номер_фотографа = int.Parse(reader.ReadElementContentAsString());
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "locationid")
                        {
                            importedDogovor.Номер_локации = int.Parse(reader.ReadElementContentAsString());
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "datestart")
                        {
                            importedDogovor.Начало_съемки = Convert.ToDateTime(reader.ReadElementContentAsString(), new CultureInfo("en-us"));
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "dateend")
                        {
                            importedDogovor.Окончание_съемки = Convert.ToDateTime(reader.ReadElementContentAsString(), new CultureInfo("en-us"));
                        }
                        if (reader.NodeType == XmlNodeType.Attribute && reader.Name == "statusid")
                        {
                            importedDogovor.Статус_договора = int.Parse(reader.ReadElementContentAsString());
                        }

                        MainWindow.db.Договор.Add(importedDogovor);
                        
                    }
                }
                MainWindow.db.SaveChanges();
                currentList = MainWindow.db.Договор.ToList();
                Refresh();
            }

        }
    }
}

