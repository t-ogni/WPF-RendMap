using RendMap.view;
using RendMap;
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
using System.Text.RegularExpressions;
using System.Data;

namespace RendMap.view
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            passwordWarn.Visibility = Visibility.Hidden;
            loginWarn.Visibility = Visibility.Hidden;

            string passwordUser = passwordBox.Password.ToString();
            string loginUser = loginBox.Text.ToString();

            if (!Regex.IsMatch(loginUser, App.loginRegex))
            {
                loginWarn.Text = "Запрещенные символы";
                loginWarn.Visibility = Visibility.Visible;
            }
            if (!Regex.IsMatch(passwordUser, App.loginRegex))
            {
                passwordWarn.Text = "Запрещенные символы";
                passwordWarn.Visibility = Visibility.Visible;
            }

            DataTable user = App.makeSQL("select * from users where login='"+ loginUser+"';");

            if (user.Rows.Count < 1)
            {
                loginWarn.Text = "Пользователь не найден";
                loginWarn.Visibility = Visibility.Visible;
            } else
            {
                if( (string) user.Rows[0]["password"] == passwordUser)
                {
                    App.user = user.Rows[0];
                    App.logged_in = true;
                    this.Hide();
                    this.Close();
                } else
                {
                    passwordWarn.Text = "Пароль неверный";
                    passwordWarn.Visibility = Visibility.Visible;
                }
            }
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
            view.register registerView = new view.register();
            registerView.ShowDialog();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text; 
            if (!Regex.IsMatch(input, App.loginRegex))
            { 
                loginWarn.Text = "Запрещенные символы";
                loginWarn.Visibility = Visibility.Visible;
            } else
            {
                loginWarn.Visibility = Visibility.Hidden;
            }
        }

        private void Password_TextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string input = (sender as PasswordBox).Password.ToString();
            if (!Regex.IsMatch(input, App.loginRegex))
            {
                passwordWarn.Text = "Запрещенные символы";
                passwordWarn.Visibility = Visibility.Visible;
            }
            else
            {
                passwordWarn.Visibility = Visibility.Hidden;
            }
        }
    }
}
