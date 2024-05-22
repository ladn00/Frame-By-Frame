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
    /// Логика взаимодействия для AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Window
    {
        private Локация location;

        public AddLocation(Локация location)
        {
            InitializeComponent();
            this.location = location;
            DataContext = location;
            cbImagePath.ItemsSource = MainWindow.db.Локация.ToList();
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbAddress.Text))
                    throw new Exception("Введите адрес");

                if (location.Номер_локации == 0)
                {
                    location.Во_владении_компании = "да";
                    MainWindow.db.Локация.Add(location);
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
                    string path = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "Pages/imgs/" +  fileTitle;
                    if (!File.Exists(path))
                        File.Copy(filename, path, true);

                    location.Изображение = fileTitle;
                    var newImgs = MainWindow.db.Локация.ToList();
                    newImgs.Add(location);
                    MainWindow.db.SaveChanges();
                    cbImagePath.ItemsSource = newImgs;
                    cbImagePath.SelectedItem = location;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
