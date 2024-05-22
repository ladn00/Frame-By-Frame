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
using System.IO;

namespace Фотостудия
{
    /// <summary>
    /// Логика взаимодействия для AddJournalWIn.xaml
    /// </summary>
    public partial class AddJournalWIn : Window
    {
        private Журнал_фотосъемки jour;
        private Договор dogovor;

        public AddJournalWIn(Журнал_фотосъемки jour, Договор dogovor)
        {
            InitializeComponent();

            this.jour = jour;
            DataContext = jour;
            this.dogovor = dogovor;

            cbImagePath.ItemsSource = MainWindow.db.Журнал_фотосъемки.ToList();
        }

        private void bt_files(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "";
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".jpg";
                dialog.Filter = "Images (*.png;*.jpg;*jpeg)|*.png;*.jpg;*jpeg";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    filename = dialog.FileName;
                    string fileTitle = System.IO.Path.GetFileName(filename);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "Pages/imgs/" + fileTitle;
                    if (!File.Exists(path))
                        File.Copy(filename, path, true);

                    jour.Изображение = fileTitle;
                    var newImgs = MainWindow.db.Журнал_фотосъемки.ToList();
                    newImgs.Add(jour);
                    MainWindow.db.SaveChanges();
                    cbImagePath.ItemsSource = newImgs;
                    cbImagePath.SelectedItem = jour;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (jour.Номер_записи == 0)
                {
                    jour.Номер_договора = dogovor.Номер_договора;
                    MainWindow.db.Журнал_фотосъемки.Add(jour);
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
