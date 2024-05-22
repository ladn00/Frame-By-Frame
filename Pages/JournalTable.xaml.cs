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
    /// Логика взаимодействия для JournalTable.xaml
    /// </summary>
    public partial class JournalTable : Page
    {
        Договор dogovor;

        public JournalTable(Договор dogovor)
        {
            InitializeComponent();
            this.dogovor = dogovor;
            List<Журнал_фотосъемки> list;
            if(dogovor != null)
            {
                list = dogovor.Журнал_фотосъемки.ToList();
                lw1.ItemsSource = list;
            }
                
        }

        /// <summary>
        /// Добавление фотографии в журнал
        /// </summary>
        private void bt_Add(object sender, RoutedEventArgs e)
        {
            Журнал_фотосъемки photo = new Журнал_фотосъемки();
            photo.Номер_записи = 0;
            AddJournalWIn edit = new AddJournalWIn(photo, dogovor);
            edit.ShowDialog();
            lw1.ItemsSource = null;
            lw1.ItemsSource = MainWindow.db.Журнал_фотосъемки.ToList().Where(p => p.Номер_договора == dogovor.Номер_договора);
        }

        /// <summary>
        /// Удаление фотографии из журнала
        /// </summary>
        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = lw1.SelectedItem as Журнал_фотосъемки;

                if (deleted != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Журнал_фотосъемки.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        lw1.ItemsSource = MainWindow.db.Журнал_фотосъемки.ToList().Where(p => p.Номер_договора == dogovor.Номер_договора);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование фотографии в журнале
        /// </summary>
        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Журнал_фотосъемки;
            AddJournalWIn edit = new AddJournalWIn(selected, dogovor);
            edit.ShowDialog();
            lw1.ItemsSource = MainWindow.db.Журнал_фотосъемки.ToList().Where(p => p.Номер_договора == dogovor.Номер_договора);
        }
    }
}
