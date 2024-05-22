//***********************************************************************
//*Название программы: "Frame By Frame"                                 *
//*                                                                     *
//*Назначение программы: для учета предоставленных услуг фотостудией,   *
//ее фотографов, клиентов, локаций,                                     *
//а также прайс-листа предоставляемых видов услуг.                      *
//*                                                                     *
//*Разработчик: студент группы ПР-330/б Зуев А.А.                       *
//*                                                                     *
//* версия: 1.0                                                         *
//***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace Фотостудия
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Клиент curClient;
        public static Фотограф curPhotog;
        public static Администратор curAdmin;

        public static ФотостудияEntities db = new ФотостудияEntities(); // БД

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Смена фокуса с "Вход" на "Регистрация" или наоборот
        /// </summary>
        void SignChange(Label lb1, Label lb2, Rectangle stroke1, Rectangle stroke2)
        {
            lb2.Foreground = Brushes.White;
            stroke2.Visibility = Visibility.Visible;

            stroke1.Visibility = Visibility.Hidden;
            lb1.Foreground = this.Resources["notActive"] as SolidColorBrush;
        }

        /// <summary>
        /// Смена фокуса с "Регистрация" на "Вход"
        /// </summary>
        private void SignIn_Click(object sender, MouseButtonEventArgs e)
        {
            SignChange(lb2, lb1, stroke2, stroke1);
        }

        /// <summary>
        /// Смена фокуса с "Вход" на "Регистрация"
        /// </summary>
        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            SignChange(lb1, lb2, stroke1, stroke2);

            Клиент client = new Клиент();
            client.Номер_клиента = 0;

            AddClientWin win = new AddClientWin(client);
            win.ShowDialog();

            SignChange(lb2, lb1, stroke2, stroke1);
        }

        /// <summary>
        /// Кнопка "Вход"
        /// </summary>
        private void SignInBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbTypeOfUser.SelectedIndex)
                {
                    case 0:
                        // Клиент
                        var user = MainWindow.db.Клиент.FirstOrDefault(p => p.Логин == tbLogin.Text && p.Пароль == tbPassword.Password);

                        if (user != null)
                        {
                            curClient = user;
                            StreamWriter sr = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Журнал.txt", true);
                            sr.WriteLine(String.Format("{0,-10} | {1, -30} | {2, -20} | {3, -10}", curClient.Логин, curClient.FIO, DateTime.Now.ToString("g"), "Клиент"));
                            sr.Close();
                            ForClientWindow win = new ForClientWindow();
                            win.Show();
                        }
                        else
                        {
                            throw new Exception("Неверный логин или пароль");
                        }
                        break;
                    case 1:
                        // Фотограф
                        var user1 = MainWindow.db.Фотограф.FirstOrDefault(p => p.Логин == tbLogin.Text && p.Пароль == tbPassword.Password);

                        if (user1 != null)
                        {
                            curPhotog = user1;
                            StreamWriter sr = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Журнал.txt", true);
                            sr.WriteLine(String.Format("{0,-10} | {1, -30} | {2, -20} | {3, -10}", curPhotog.Логин, curPhotog.FIO, DateTime.Now.ToString("g"), "Фотограф"));
                            sr.Close();
                            ForPhotographerWin win = new ForPhotographerWin();
                            win.Show();
                        }
                        else
                        {
                            throw new Exception("Неверный логин или пароль");
                        }
                        break;

                    case 2:
                        // Администратор
                        var user2 = MainWindow.db.Администратор.FirstOrDefault(p => p.Логин == tbLogin.Text && p.Пароль == tbPassword.Password);

                        if (user2 != null)
                        {
                            curAdmin = user2;
                            StreamWriter sr = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Журнал.txt", true);
                            sr.WriteLine(String.Format("{0,-10} | {1, -30} | {2, -20} | {3, -10}", curAdmin.Логин, "Администратор", DateTime.Now.ToString("g"), "Админ"));
                            sr.Close();
                            ForAdminWindow win2 = new ForAdminWindow();
                            win2.Show();
                        }
                        else
                        {
                            throw new Exception("Неверный логин или пароль");
                        }
                        break;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Поле для пароля потеряло фокус
        /// </summary>
        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Password == "")
            {
                tbl.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Поле для пароля получило
        /// </summary>
        private void passwordFocus_Changed(object sender, RoutedEventArgs e)
        {
            tbl.Visibility = Visibility.Hidden;
        }

    }
}
