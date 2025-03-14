using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace ChildPlayCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, заполните логин и пароль!");
                return;
            }

            try
            {
                using (var context = new PlayCenterContext())
                {
                    string login = LoginTextBox.Text;
                    string password = PasswordBox.Password;
                    string passwordHash = ComputeMd5Hash(password);

                    var user = context.Persons
                        .Include(p => p.Role)
                        .FirstOrDefault(p => p.Login == login && p.PasswordHash == passwordHash && p.DeletedAt == null);

                    if (user == null)
                    {
                        MessageBox.Show("Неверный логин или пароль!");
                        return;
                    }

                    Page targetPage = user.Role?.Name switch
                    {
                        "клиент" => new ClientPage(user.Id),
                        "администратор" => new AdminPage(),
                        "менеджер" => new ManagerPage(),
                        "аниматор" => new AnimatorPage(),
                        "оператор автоматов" => new OperatorPage(),
                        _ => null
                    };

                    if (targetPage != null)
                    {
                        NavigationService.Navigate(targetPage);
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная роль!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuestPage());
        }

        private string ComputeMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
