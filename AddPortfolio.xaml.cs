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
    /// Логика взаимодействия для AddPortfolio.xaml
    /// </summary>
    public partial class AddPortfolio : Window
    {
        Портфолио_фотографа port;
        Фотограф photog;
        public AddPortfolio(Фотограф photog, Портфолио_фотографа port)
        {
            InitializeComponent();
            this.port = port;
            this.photog = photog;
            DataContext = port;
            cbImagePath.ItemsSource = MainWindow.db.Портфолио_фотографа.ToList();
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                port.Номер_фотографа = photog.Табельный_номер;

                if (port.Номер_портфолио == 0)
                {
                    MainWindow.db.Портфолио_фотографа.Add(port);
                }

                MainWindow.db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                    port.Изображение = fileTitle;
                    var newImgs = MainWindow.db.Портфолио_фотографа.ToList();
                    newImgs.Add(port);
                    MainWindow.db.SaveChanges();
                    cbImagePath.ItemsSource = newImgs;
                    cbImagePath.SelectedItem = port;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
