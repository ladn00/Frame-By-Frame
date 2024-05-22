using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для RegistrationJournal.xaml
    /// </summary>
    public partial class RegistrationJournal : Page
    {
        string txtPath = AppDomain.CurrentDomain.BaseDirectory + "Журнал.txt";
        string[] list;
        public RegistrationJournal()
        {
            InitializeComponent();
            list = File.ReadAllLines(txtPath);
            if (list != null && list.Length != 0)
            {
                foreach(string s in list)
                {
                    lw1.Items.Add($"{s}");
                }
            }
        }
    }
}
