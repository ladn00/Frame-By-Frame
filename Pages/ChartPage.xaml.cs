using System;
using System.Collections.Generic;
using System.Drawing;
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
using ScottPlot.WPF;
using ScottPlot.Colormaps;
using ScottPlot;
using ScottPlot.Plottables;
using System.Diagnostics;

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();
            rb1.IsChecked = true;
            DatesForOtchet();
            var baza = MainWindow.db.Вид_услуги.ToList();
            countY = new double[baza.Count];
            int index = 0;
            Tick[]ticks = new Tick[baza.Count];


            foreach (Вид_услуги t in baza)
            {
                if (t.Название.Length > 18)
                    ticks[index] = new Tick(index, t.Название.Substring(0, 18) + ".");
                else
                    ticks[index] = new Tick(index, t.Название);
                countY[index] = (t.Договор.ToList().Count);
                index++;
            }

            NewChart(ticks);
            WpfPlot1.Plot.Axes.Bottom.Label.Text = "Виды услуг";
            WpfPlot1.Plot.Axes.Title.Label.Text = $"Популярность видов услуг за весь период";
        }

        /// <summary>
        /// Смена на произвольный выбор даты
        /// </summary>
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            cb2.Visibility = Visibility.Collapsed;
            sp1.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Смена на выбор из системных дат
        /// </summary>
        private void rb2_Chcked(object sender, RoutedEventArgs e)
        {
            cb2.Visibility = Visibility.Visible;
            sp1.Visibility = Visibility.Collapsed;
        }

        DateTime dateFrom, dateTo;
        double[] countY;

        /// <summary>
        /// Применение настроек для диаграммы
        /// </summary>
        private void chart_Click(object sender, RoutedEventArgs e)
        {
            
            int index = 0;
            Tick[] ticks;
            switch (cb1.SelectedIndex)
            {
                case 0:
                    DatesForOtchet();
                    var baza = MainWindow.db.Вид_услуги.ToList();
                    countY = new double[baza.Count];
                    index = 0;
                    ticks = new Tick[baza.Count];
                    
                    foreach (Вид_услуги t in baza)
                    {
                        if (t.Название.Length > 18)
                            ticks[index] = new Tick(index, t.Название.Substring(0, 18) + ".");
                        else
                            ticks[index] = new Tick(index, t.Название);

                        countY[index] = (t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList().Count);
                        index++;
                    }

                    NewChart(ticks);
                    WpfPlot1.Plot.Axes.Bottom.Label.Text = "Виды услуг";
                    WpfPlot1.Plot.Axes.Title.Label.Text = $"Популярность видов услуг за {dateFrom:d} - {dateTo:d}";
                    break;

                case 1:

                    DatesForOtchet();
                    var baza1 = MainWindow.db.Фотограф.ToList();
                    countY = new double[baza1.Count];
                    index = 0;
                    ticks = new Tick[baza1.Count];

                    foreach (Фотограф t in baza1)
                    {
                        ticks[index] = new Tick(index, t.FIO);
                        countY[index] = (t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList().Count);
                        index++;
                    }

                    NewChart(ticks);
                    WpfPlot1.Plot.Axes.Bottom.Label.Text = "Фотографы";
                    WpfPlot1.Plot.Axes.Title.Label.Text = $"Популярность фотографов за {dateFrom:d} - {dateTo:d}";

                    break;

                case 2:

                    DatesForOtchet();
                    var baza2 = MainWindow.db.Локация.Where(p=> p.Во_владении_компании.Trim() == "да").ToList();
                    countY = new double[baza2.Count];
                    index = 0;
                    ticks = new Tick[baza2.Count];

                    foreach (Локация t in baza2)
                    {
                        if (t.Адрес.Length > 18)
                            ticks[index] = new Tick(index, t.Адрес.Substring(0, 18) + ".");
                        else
                            ticks[index] = new Tick(index, t.Адрес);
                        countY[index] = (t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList().Count);
                        index++;
                    }

                    NewChart(ticks);
                    WpfPlot1.Plot.Axes.Title.Label.Text = $"Популярность локаций за {dateFrom:d} - {dateTo:d}";
                    WpfPlot1.Plot.Axes.Bottom.Label.Text = "Локации";

                    break;

                default:
                    MessageBox.Show("Выберите настройки формирования отчета");
                    break;
            }
        }

        Random ran = new Random();
        ScottPlot.Palettes.Category10 palette = new ScottPlot.Palettes.Category10();

        /// <summary>
        /// Метод создания новой диаграммы
        /// </summary>
        public void NewChart(Tick[] ticks)
        {
            
            WpfPlot1.Plot.Clear();
            var barPlot = WpfPlot1.Plot.Add.Bars(countY);
            WpfPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            WpfPlot1.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleCenter;
            WpfPlot1.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            int index = 1;
            foreach (var bar in barPlot.Bars)
            {
                bar.Label = bar.Value.ToString();
                bar.Value = (int)bar.Value;
                bar.FillColor = palette.GetColor(index);
                index++;
            }
            
            WpfPlot1.Plot.ShowLegend(Alignment.UpperLeft);
            WpfPlot1.Plot.ShowLegend();
            WpfPlot1.Plot.Axes.Left.Label.Text = "Количество заказов\n";
       
            WpfPlot1.Refresh();
        }

        /// <summary>
        /// Даты для диаграммы
        /// </summary>
        public void DatesForOtchet()
        {
            if (rb1.IsChecked == true)
            {
                DateTime.TryParse(date1.Text, out dateFrom);
                DateTime.TryParse(date2.Text, out dateTo);

            }
            else
            {
                switch (cb2.SelectedIndex)
                {
                    case 0:
                        dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
                        dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1));
                        break;
                    case 1:
                        dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                        break;
                    case 2:
                        dateFrom = new DateTime(DateTime.Now.Year - 1, 1, 1);
                        dateTo = new DateTime(DateTime.Now.Year - 1, 12, 31);
                        break;
                    case 3:
                        dateFrom = new DateTime(DateTime.Now.Year, 1, 1);
                        dateTo = new DateTime(DateTime.Now.Year, 12, 31);
                        break;
                    default:
                        MessageBox.Show("Задайте период формируемого отчета");
                        break;
                }
            }
        }
    }
}
