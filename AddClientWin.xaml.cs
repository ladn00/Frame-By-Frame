using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Фотостудия
{
    /// <summary>
    /// Логика взаимодействия для AddClientWin.xaml
    /// </summary>
    public partial class AddClientWin : Window
    {
        private Клиент client;

        public AddClientWin(Клиент client)
        {
            InitializeComponent();
            this.client = client;
            DataContext = client;
        }

        private void bt_SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbFamilia.Text) || string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbPassportN.Text) ||
                        string.IsNullOrWhiteSpace(tbPassportS.Text) || string.IsNullOrWhiteSpace(tbBirthdate.Text) || string.IsNullOrWhiteSpace(tbPhone.Text) ||
                        string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPassword.Text))
                    throw new Exception("Заполните все поля!");

                if (!Regex.IsMatch(tbPhone.Text, @"^\+\d{11}$"))
                    throw new Exception("Неверный номер телефона");

                if (!Regex.IsMatch(tbPassword.Text, @"^(?=.*[A-Za-zА-Яа-я]*)(?=.*\d*)(?=.*[*@#.$%&^]*).{4,}$"))
                    throw new Exception("Длина пароля должна быть не менее 4-ех символов. Допустимые символы: А-я A-z *@#.$%&^");

                if (!Regex.IsMatch(tbPassportN.Text, @"^\d{4}$"))
                    throw new Exception("Номер паспорта содержит 4 цифры");

                if (MainWindow.db.Клиент.Where(x => x.Логин == tbLogin.Text && x.Номер_клиента != client.Номер_клиента).Count() > 0)
                    throw new Exception("Логин занят");

                if (!Regex.IsMatch(tbPassportS.Text, @"^\d{6}$"))
                    throw new Exception("Серия паспорта содержит 6 цифр");

                if (!Regex.IsMatch(tbLogin.Text, @"^(?=.{4,})(?=.*[A-Za-zА-Яа-я]).*$"))
                    throw new Exception("Логин должен состоять из не менее 4-ех символов и содержать букву");

                if (!Regex.IsMatch(tbFamilia.Text, @"^(?=.{1,})(?=.*[A-Za-zА-Яа-я]).*$"))
                    throw new Exception("Фамилия может содержить только буквы");

                if (!Regex.IsMatch(tbName.Text, @"^(?=.{1,})(?=.*[A-Za-zА-Яа-я]).*$"))
                    throw new Exception("Имя может содержить только буквы");

                if (!Regex.IsMatch(tbOtchestvo.Text, @"^(?=.{1,})(?=.*[A-Za-zА-Яа-я]).*$"))
                    throw new Exception("Отчество может содержить только буквы");

                if (client.Номер_клиента == 0)
                {
                    MainWindow.db.Клиент.Add(client);
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
