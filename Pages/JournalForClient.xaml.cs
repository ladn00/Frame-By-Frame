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

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для JournalForClient.xaml
    /// </summary>
    public partial class JournalForClient : Page
    {
        Договор dogovor;
        public JournalForClient(Договор dogovor)
        {
            InitializeComponent();
            this.dogovor = dogovor; 
            List<Журнал_фотосъемки> list;
            if (dogovor != null)
            {
                list = dogovor.Журнал_фотосъемки.ToList();
                lw1.ItemsSource = list;
            }
        }
    }
}
