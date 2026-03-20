using ShoeShop.DbContexts;
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
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWin.xaml
    /// </summary>
    public partial class LoginWin : Window
    {

        public LoginWin()
        {
            InitializeComponent();
        }

        private void GuestTb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentUser.Id = 0;
            CurrentUser.Role = "Гость";
            CurrentUser.FullName = "Гость";
            OpenProdWin();
        }

        private void OpenProdWin()
        {
            CatalogWindow mn = new();
            mn.Show();
            Close();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginTB.Text.Trim();
            string passw = PasswTB.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passw))
            {
                MessageBox.Show("Введите email и пароль");
                return;
            }

            using var db = new ShoeshopContext() ;
            
                var user = db.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email == email && u.Passw == passw);

                if (user == null)
                {
                    MessageBox.Show("Неверный email или пароль");
                    return;
                }

                CurrentUser.Id = user.IdUser;
                CurrentUser.FullName = $"{user.LastName} {user.FirstName} {user.MiddleName}";
                CurrentUser.Role = user.Role.RoleName;

                OpenProdWin();
            
        }
    }
}
