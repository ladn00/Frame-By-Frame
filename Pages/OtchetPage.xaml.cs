using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
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
using Excel = Microsoft.Office.Interop.Excel;
using Page = System.Windows.Controls.Page;
using word = Microsoft.Office.Interop.Word;

namespace Фотостудия.Pages
{
    /// <summary>
    /// Логика взаимодействия для OtchetPage.xaml
    /// </summary>
    public partial class OtchetPage : Page
    {
        public OtchetPage()
        {
            InitializeComponent();
            rb1.IsChecked = true;
        }

        /// <summary>
        /// Произвольные даты
        /// </summary>
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            cb2.Visibility = Visibility.Collapsed;
            sp1.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Системные даты
        /// </summary>
        private void rb2_Chcked(object sender, RoutedEventArgs e)
        {
            cb2.Visibility = Visibility.Visible;
            sp1.Visibility = Visibility.Collapsed;
        }

        DateTime dateFrom = new DateTime(), dateTo = new DateTime();

        /// <summary>
        /// Формирование Excel-отчета
        /// </summary>
        private void bt_excel(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp;
            Excel.Workbook workbook;
            Excel.Range rangeTypeName, rangeAllTable, rangeHeader;
            Excel.Worksheet worksheet;
            
            int index = 1;

            switch (cb1.SelectedIndex)
            {
                case 0:
                    DatesForOtchet();
                    excelApp = new Excel.Application();
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    excelApp.Visible = true;
                    excelApp.UserControl = true;

                    worksheet.Cells[1, 1] = "Клиент";
                    worksheet.Cells[1, 2] = "Фотограф";
                    worksheet.Cells[1, 3] = "Локация";
                    worksheet.Cells[1, 4] = "Дата начала";
                    worksheet.Cells[1, 5] = "Дата окончания";
                    worksheet.Cells[1, 6] = "Статус";
                    worksheet.Cells[1, 7] = "Стоимость";
                    rangeTypeName = worksheet.Range["A2:G2"];
                    rangeTypeName.Merge();
                    index = 1;
                    
                    var baza = MainWindow.db.Вид_услуги.ToList();
                    
                    var list = baza[0].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                    for (int j = 0; j < baza.Count; j++)
                    {
                        index++;
                        worksheet.Cells[index, 1] = baza[j].Название;
                        rangeTypeName = worksheet.Range["A" + index + ":" + "G" + index];
                        rangeTypeName.Interior.Color = Excel.XlRgbColor.rgbBisque;
                        rangeTypeName.Merge();
                        rangeTypeName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        list = baza[j].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                        if(list.Count == 0)
                        {
                            index++;
                            worksheet.Range["A" + index + ":G" + index].Merge();
                            worksheet.Cells[index, 1] = "Нет записей";
                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            index++;
                            worksheet.Cells[index, 1] = list[i].Клиент.FIO;
                            worksheet.Cells[index, 2] = list[i].Фотограф.FIO;
                            worksheet.Cells[index, 3] = list[i].Локация.Адрес;
                            worksheet.Cells[index, 4] = list[i].Начало_съемки;
                            worksheet.Cells[index, 5] = list[i].Окончание_съемки;
                            worksheet.Cells[index, 6] = list[i].Статус.Наименование;
                            worksheet.Cells[index, 7] = list[i].Стоимость.ToString("f2") + " руб.";
                        }
                    }

                    rangeAllTable = worksheet.Range["A1:G" + index];
                    rangeHeader = worksheet.Range["A1:G1"];
                    rangeAllTable.EntireColumn.AutoFit();
                    rangeAllTable.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    rangeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rangeHeader.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                    break;

                case 1:
                    DatesForOtchet();
                    excelApp = new Excel.Application();
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    excelApp.Visible = true;
                    excelApp.UserControl = true;

                    worksheet.Cells[1, 1] = "Клиент";
                    worksheet.Cells[1, 2] = "Услуга";
                    worksheet.Cells[1, 3] = "Локация";
                    worksheet.Cells[1, 4] = "Дата начала";
                    worksheet.Cells[1, 5] = "Дата окончания";
                    worksheet.Cells[1, 6] = "Статус";
                    worksheet.Cells[1, 7] = "Стоимость";
                    rangeTypeName = worksheet.Range["A2:G2"];
                    rangeTypeName.Merge();
                    index = 1;
                    var baza1 = MainWindow.db.Фотограф.ToList();
                    
                    var list1 = baza1[0].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                    for (int j = 0; j < baza1.Count; j++)
                    {
                        index++;
                        worksheet.Cells[index, 1] = baza1[j].FIO;
                        rangeTypeName = worksheet.Range["A" + index + ":" + "G" + index];
                        rangeTypeName.Interior.Color = Excel.XlRgbColor.rgbBisque;
                        rangeTypeName.Merge();
                        rangeTypeName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        list1 = baza1[j].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                        if (list1.Count == 0)
                        {
                            index++;
                            worksheet.Range["A" + index + ":G" + index].Merge();
                            worksheet.Cells[index, 1] = "Нет записей";
                        }

                        for (int i = 0; i < list1.Count; i++)
                        {
                            index++;
                            worksheet.Cells[index, 1] = list1[i].Клиент.FIO;
                            worksheet.Cells[index, 2] = list1[i].Вид_услуги1.Название;
                            worksheet.Cells[index, 3] = list1[i].Локация.Адрес;
                            worksheet.Cells[index, 4] = list1[i].Начало_съемки;
                            worksheet.Cells[index, 5] = list1[i].Окончание_съемки;
                            worksheet.Cells[index, 6] = list1[i].Статус.Наименование;
                            worksheet.Cells[index, 7] = list1[i].Стоимость.ToString("f2") + " руб.";
                        }
                    }

                    rangeAllTable = worksheet.Range["A1:G" + index];
                    rangeHeader = worksheet.Range["A1:G1"];
                    rangeAllTable.EntireColumn.AutoFit();
                    rangeAllTable.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    rangeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rangeHeader.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    break;

                case 2:
                    DatesForOtchet();
                    excelApp = new Excel.Application();
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    excelApp.Visible = true;
                    excelApp.UserControl = true;

                    worksheet.Cells[1, 1] = "Клиент";
                    worksheet.Cells[1, 2] = "Услуга";
                    worksheet.Cells[1, 3] = "Фотограф";
                    worksheet.Cells[1, 4] = "Дата начала";
                    worksheet.Cells[1, 5] = "Дата окончания";
                    worksheet.Cells[1, 6] = "Статус";
                    worksheet.Cells[1, 7] = "Стоимость";
                    rangeTypeName = worksheet.Range["A2:G2"];
                    rangeTypeName.Merge();
                    index = 1;
                    var baza2 = MainWindow.db.Локация.Where(p=>p.Во_владении_компании.Trim() == "да").ToList();
                    
                    var list2 = baza2[0].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                    for (int j = 0; j < baza2.Count; j++)
                    {
                        index++;
                        worksheet.Cells[index, 1] = baza2[j].Адрес;
                        rangeTypeName = worksheet.Range["A" + index + ":" + "G" + index];
                        rangeTypeName.Interior.Color = Excel.XlRgbColor.rgbBisque;
                        rangeTypeName.Merge();
                        rangeTypeName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        list2 = baza2[j].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                        if (list2.Count == 0)
                        {
                            index++;
                            worksheet.Range["A" + index + ":G" + index].Merge();
                            worksheet.Cells[index, 1] = "Нет записей";
                        }

                        for (int i = 0; i < list2.Count; i++)
                        {
                            index++;
                            worksheet.Cells[index, 1] = list2[i].Клиент.FIO;
                            worksheet.Cells[index, 2] = list2[i].Вид_услуги1.Название;
                            worksheet.Cells[index, 3] = list2[i].Фотограф.FIO;
                            worksheet.Cells[index, 4] = list2[i].Начало_съемки;
                            worksheet.Cells[index, 5] = list2[i].Окончание_съемки;
                            worksheet.Cells[index, 6] = list2[i].Статус.Наименование;
                            worksheet.Cells[index, 7] = list2[i].Стоимость.ToString("f2") + " руб.";
                        }
                    }

                    rangeAllTable = worksheet.Range["A1:G" + index];
                    rangeHeader = worksheet.Range["A1:G1"];
                    rangeAllTable.EntireColumn.AutoFit();
                    rangeAllTable.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    rangeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rangeHeader.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    break;

                case 3:
                    DatesForOtchet();
                    excelApp = new Excel.Application();
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    excelApp.Visible = true;
                    excelApp.UserControl = true;

                    worksheet.Cells[1, 1] = "Клиент";
                    worksheet.Cells[1, 2] = "Услуга";
                    worksheet.Cells[1, 3] = "Локация";
                    worksheet.Cells[1, 4] = "Дата начала";
                    worksheet.Cells[1, 5] = "Дата окончания";
                    worksheet.Cells[1, 6] = "Статус";
                    worksheet.Cells[1, 7] = "Стоимость";
                    rangeTypeName = worksheet.Range["A2:G2"];
                    rangeTypeName.Merge();
                    index = 1;
                    var baza3 = MainWindow.db.Фотограф.ToList();

                    var list3 = baza3[0].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                    for (int j = 0; j < baza3.Count; j++)
                    {
                        index++;
                        worksheet.Cells[index, 1] = baza3[j].FIO;
                        rangeTypeName = worksheet.Range["A" + index + ":" + "G" + index];
                        rangeTypeName.Interior.Color = Excel.XlRgbColor.rgbBisque;
                        rangeTypeName.Merge();
                        rangeTypeName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        list3 = baza3[j].Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();

                        if (list3.Count == 0)
                        {
                            index++;
                            worksheet.Range["A" + index + ":G" + index].Merge();
                            worksheet.Cells[index, 1] = "Нет записей";
                        }
                        else
                        {
                            decimal sum = 0;
                            for (int i = 0; i < list3.Count; i++)
                            {
                                sum += list3[i].Стоимость;
                                index++;
                                worksheet.Cells[index, 1] = list3[i].Клиент.FIO;
                                worksheet.Cells[index, 2] = list3[i].Вид_услуги1.Название;
                                worksheet.Cells[index, 3] = list3[i].Локация.Адрес;
                                worksheet.Cells[index, 4] = list3[i].Начало_съемки;
                                worksheet.Cells[index, 5] = list3[i].Окончание_съемки;
                                worksheet.Cells[index, 6] = list3[i].Статус.Наименование;
                                worksheet.Cells[index, 7] = list3[i].Стоимость.ToString("f2") + " руб.";
                            }

                            Excel.Range range;
                            index++;
                            worksheet.Cells[index, 6] = "Выплата фотографу:";
                            worksheet.Cells[index, 7] = (sum * 0.6m).ToString("f2") + " руб.";

                            index++;
                            worksheet.Cells[index, 6] = "Прибыль фотостудии:";
                            worksheet.Cells[index, 7] = (sum * 0.4m).ToString("f2") + " руб.";

                            index++;
                            worksheet.Cells[index, 6] = "Итого:";
                            worksheet.Cells[index, 7] = sum.ToString("f2") + " руб.";
                            range = worksheet.Range["F" + (index - 2) + ":G" + index];
                            range.Font.Bold = true;
                            range.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        }
                        
                    }

                    rangeAllTable = worksheet.Range["A1:G" + index];
                    rangeHeader = worksheet.Range["A1:G1"];
                    rangeAllTable.EntireColumn.AutoFit();
                    rangeAllTable.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    rangeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rangeHeader.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    break;
                default:
                    MessageBox.Show("Выберите настройки формирования отчета");
                    break;
            }

        }

        /// <summary>
        /// Определение дат для отчета
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

        word.Application app;
        word.Document doc;
        word.Paragraph p1, newParagraph, tableParagraph;
        word.Range newRange, newRange2, cellRange;
        word.Table newTable;

        /// <summary>
        /// Формирование Word-отчета
        /// </summary>
        private void bt_word(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cb1.SelectedIndex)
                {
                    case 0:
                        NewWordApp("предоставленным услугам");

                        var baza = MainWindow.db.Вид_услуги.ToList();

                        foreach (Вид_услуги t in baza)
                        {
                            var dogovors = t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();
                            newParagraph = doc.Paragraphs.Add();
                            newRange = newParagraph.Range;
                            newRange.Text = "\n" + t.Название;
                            newRange.InsertParagraphAfter();

                            tableParagraph = doc.Paragraphs.Add();
                            newRange2 = tableParagraph.Range;
                            newTable = doc.Tables.Add(newRange2, dogovors.Count + 1, 7);
                            newTable.Borders.InsideLineStyle = newTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                            newTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            cellRange = newTable.Cell(1, 1).Range;
                            cellRange.Text = "Клиент";
                            cellRange = newTable.Cell(1, 2).Range;
                            cellRange.Text = "Фотограф";
                            cellRange = newTable.Cell(1, 3).Range;
                            cellRange.Text = "Локация";
                            cellRange = newTable.Cell(1, 4).Range;
                            cellRange.Text = "Дата начала";
                            cellRange = newTable.Cell(1, 5).Range;
                            cellRange.Text = "Дата окончания";
                            cellRange = newTable.Cell(1, 6).Range;
                            cellRange.Text = "Статус";
                            cellRange = newTable.Cell(1, 7).Range;
                            cellRange.Text = "Стоимость";

                            newTable.Rows[1].Range.Bold = 1;
                            newTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;



                            for (int i = 0; i < dogovors.Count; i++)
                            {
                                var currentTour = dogovors[i];
                                cellRange = newTable.Cell(i + 2, 1).Range;
                                cellRange.Text = currentTour.Клиент.FIO;

                                cellRange = newTable.Cell(i + 2, 2).Range;
                                cellRange.Text = currentTour.Фотограф.FIO;

                                cellRange = newTable.Cell(i + 2, 3).Range;
                                cellRange.Text = currentTour.Локация.Адрес;

                                cellRange = newTable.Cell(i + 2, 4).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 5).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 6).Range;
                                cellRange.Text = currentTour.Статус.Наименование;

                                cellRange = newTable.Cell(i + 2, 7).Range;
                                cellRange.Text = currentTour.Стоимость.ToString("f2") + " руб.";

                            }
                        }
                        break;

                    case 1:
                        NewWordApp("занятности фотографов");

                        var baza1 = MainWindow.db.Фотограф.ToList();

                        foreach (Фотограф t in baza1)
                        {
                            var dogovors = t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();
                            newParagraph = doc.Paragraphs.Add();
                            newRange = newParagraph.Range;
                            newRange.Text = "\n" + t.FIO;
                            newRange.InsertParagraphAfter();

                            tableParagraph = doc.Paragraphs.Add();
                            newRange2 = tableParagraph.Range;
                            newTable = doc.Tables.Add(newRange2, dogovors.Count + 1, 7);
                            newTable.Borders.InsideLineStyle = newTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                            newTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            cellRange = newTable.Cell(1, 1).Range;
                            cellRange.Text = "Клиент";
                            cellRange = newTable.Cell(1, 2).Range;
                            cellRange.Text = "Услуга";
                            cellRange = newTable.Cell(1, 3).Range;
                            cellRange.Text = "Локация";
                            cellRange = newTable.Cell(1, 4).Range;
                            cellRange.Text = "Дата начала";
                            cellRange = newTable.Cell(1, 5).Range;
                            cellRange.Text = "Дата окончания";
                            cellRange = newTable.Cell(1, 6).Range;
                            cellRange.Text = "Статус";
                            cellRange = newTable.Cell(1, 7).Range;
                            cellRange.Text = "Стоимость";

                            newTable.Rows[1].Range.Bold = 1;
                            newTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;


                            for (int i = 0; i < dogovors.Count; i++)
                            {
                                var currentTour = dogovors[i];
                                cellRange = newTable.Cell(i + 2, 1).Range;
                                cellRange.Text = currentTour.Клиент.FIO;

                                cellRange = newTable.Cell(i + 2, 2).Range;
                                cellRange.Text = currentTour.Вид_услуги1.Название;

                                cellRange = newTable.Cell(i + 2, 3).Range;
                                cellRange.Text = currentTour.Локация.Адрес;

                                cellRange = newTable.Cell(i + 2, 4).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 5).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 6).Range;
                                cellRange.Text = currentTour.Статус.Наименование;

                                cellRange = newTable.Cell(i + 2, 7).Range;
                                cellRange.Text = currentTour.Стоимость.ToString("f2") + " руб.";
                            }
                        }
                        break;

                    case 2:
                        NewWordApp("используемости локаций");

                        var baza2 = MainWindow.db.Локация.Where(p => p.Во_владении_компании.Trim() == "да").ToList();

                        foreach (Локация t in baza2)
                        {
                            var dogovors = t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();
                            newParagraph = doc.Paragraphs.Add();
                            newRange = newParagraph.Range;
                            newRange.Text = "\n" + t.Адрес;
                            newRange.InsertParagraphAfter();

                            tableParagraph = doc.Paragraphs.Add();
                            newRange2 = tableParagraph.Range;
                            newTable = doc.Tables.Add(newRange2, dogovors.Count + 1, 7);
                            newTable.Borders.InsideLineStyle = newTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                            newTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            cellRange = newTable.Cell(1, 1).Range;
                            cellRange.Text = "Клиент";
                            cellRange = newTable.Cell(1, 2).Range;
                            cellRange.Text = "Услуга";
                            cellRange = newTable.Cell(1, 3).Range;
                            cellRange.Text = "Фотограф";
                            cellRange = newTable.Cell(1, 4).Range;
                            cellRange.Text = "Дата начала";
                            cellRange = newTable.Cell(1, 5).Range;
                            cellRange.Text = "Дата окончания";
                            cellRange = newTable.Cell(1, 6).Range;
                            cellRange.Text = "Статус";
                            cellRange = newTable.Cell(1, 7).Range;
                            cellRange.Text = "Стоимость";

                            newTable.Rows[1].Range.Bold = 1;
                            newTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;


                            for (int i = 0; i < dogovors.Count; i++)
                            {
                                var currentTour = dogovors[i];
                                cellRange = newTable.Cell(i + 2, 1).Range;
                                cellRange.Text = currentTour.Клиент.FIO;

                                cellRange = newTable.Cell(i + 2, 2).Range;
                                cellRange.Text = currentTour.Вид_услуги1.Название;

                                cellRange = newTable.Cell(i + 2, 3).Range;
                                cellRange.Text = currentTour.Фотограф.FIO;

                                cellRange = newTable.Cell(i + 2, 4).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 5).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 6).Range;
                                cellRange.Text = currentTour.Статус.Наименование;

                                cellRange = newTable.Cell(i + 2, 7).Range;
                                cellRange.Text = currentTour.Стоимость.ToString("f2") + " руб.";

                            }
                        }


                        break;
                    case 3:
                        NewWordApp("прибыли фотостудии");

                        var baza3 = MainWindow.db.Фотограф.ToList();

                        foreach (Фотограф t in baza3)
                        {
                            var dogovors = t.Договор.Where(p => p.Начало_съемки <= dateTo && p.Начало_съемки >= dateFrom).ToList();
                            newParagraph = doc.Paragraphs.Add();
                            newRange = newParagraph.Range;
                            newRange.Text = "\n" + t.FIO;
                            newRange.InsertParagraphAfter();

                            tableParagraph = doc.Paragraphs.Add();
                            newRange2 = tableParagraph.Range;
                            newTable = doc.Tables.Add(newRange2, dogovors.Count + 4, 7);
                            newTable.Borders.InsideLineStyle = newTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                            newTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                            cellRange = newTable.Cell(1, 1).Range;
                            cellRange.Text = "Клиент";
                            cellRange = newTable.Cell(1, 2).Range;
                            cellRange.Text = "Услуга";
                            cellRange = newTable.Cell(1, 3).Range;
                            cellRange.Text = "Локация";
                            cellRange = newTable.Cell(1, 4).Range;
                            cellRange.Text = "Дата начала";
                            cellRange = newTable.Cell(1, 5).Range;
                            cellRange.Text = "Дата окончания";
                            cellRange = newTable.Cell(1, 6).Range;
                            cellRange.Text = "Статус";
                            cellRange = newTable.Cell(1, 7).Range;
                            cellRange.Text = "Стоимость";

                            newTable.Rows[1].Range.Bold = 1;
                            newTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;

                            decimal sum = 0;
                            int i = 0;
                            for (i = 0; i < dogovors.Count; i++)
                            {
                                var currentTour = dogovors[i];
                                cellRange = newTable.Cell(i + 2, 1).Range;
                                cellRange.Text = currentTour.Клиент.FIO;

                                cellRange = newTable.Cell(i + 2, 2).Range;
                                cellRange.Text = currentTour.Вид_услуги1.Название;

                                cellRange = newTable.Cell(i + 2, 3).Range;
                                cellRange.Text = currentTour.Локация.Адрес;

                                cellRange = newTable.Cell(i + 2, 4).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 5).Range;
                                cellRange.Text = currentTour.Начало_съемки.ToString();

                                cellRange = newTable.Cell(i + 2, 6).Range;
                                cellRange.Text = currentTour.Статус.Наименование;

                                cellRange = newTable.Cell(i + 2, 7).Range;
                                cellRange.Text = currentTour.Стоимость.ToString("f2") + " руб.";
                                sum += currentTour.Стоимость;
                            }
                            cellRange = newTable.Cell(i + 2, 6).Range;
                            cellRange.Text = "Выплата фотографу:";
                            cellRange.Font.Bold = 1;
                            cellRange = newTable.Cell(i + 2, 7).Range;
                            cellRange.Text = (sum * 0.6m).ToString("f2") + " руб.";
                            cellRange.Font.Bold = 1;

                            cellRange = newTable.Cell(i + 3, 6).Range;
                            cellRange.Text = "Прибыль фотостудии:";
                            cellRange.Font.Bold = 1;
                            cellRange = newTable.Cell(i + 3, 7).Range;
                            cellRange.Text = (sum * 0.4m).ToString("f2") + " руб.";
                            cellRange.Font.Bold = 1;

                            cellRange = newTable.Cell(i + 4, 6).Range;
                            cellRange.Text = "Итого:";
                            cellRange.Font.Bold = 1;
                            cellRange = newTable.Cell(i + 4, 7).Range;
                            cellRange.Text = (sum).ToString("f2") + " руб.";
                            cellRange.Font.Bold = 1;

                        }
                        break;

                    default:
                        MessageBox.Show("Выберите настройки формирования отчета");
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Создание нового word-файла
        /// </summary>
        public void NewWordApp(string header)
        {
            DatesForOtchet();
            app = new word.Application();
            doc = new word.Document();
            app.Visible = true;
            doc.PageSetup.Orientation = word.WdOrientation.wdOrientLandscape;
            doc.PageSetup.LeftMargin = app.CentimetersToPoints(1f);
            p1 = doc.Paragraphs[1];
            p1.Range.Text = "Отчет по " + header;
            p1.Range.Bold = 1;
            p1.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
            p1.Range.Font.Size = 16f;
            p1.Range.Font.Color = word.WdColor.wdColorDarkRed;
            doc.Paragraphs.Add();
            doc.Paragraphs[2].Range.Text = "Дата: " + DateTime.Today.ToLongDateString();
            doc.Paragraphs[2].Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
            doc.Paragraphs[2].Range.Font.Color = word.WdColor.wdColorBlack;
            doc.Paragraphs[2].Range.Bold = 0;
            doc.Paragraphs[2].Range.Font.Size = 12f;
            
        }
    }
}
