using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
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
using System.Xml.Linq;

namespace RendMap.view
{
    /// <summary>
    /// Логика взаимодействия для messenger.xaml
    /// </summary>
    public partial class messenger : Window
    {
        DataRow reciever = null;
        public messenger(int u_id = -1)
        {
            InitializeComponent();

            if (App.logged_in)
            {
                updateUsers();
                if (u_id != -1) // write to this user
                {
                    reciever = App.makeSQL("select * from users where id=" + (u_id.ToString()) + ";").Rows[0];
                    // MessageBox.Show(reciever["name"].ToString(), "test");
                    updateMessages();
                } else
                {
                    messageBox.IsEnabled = false;
                    sendButton.IsEnabled = false;
                }
            }
            else
            {
                messageBox.IsEnabled = false;
                sendButton.IsEnabled = false;
                
            }
        }
        private void updateMessages()
        {
            if (reciever != null)
            {
                string users = "(" + (App.user["id"].ToString()) + ", " + (reciever["id"].ToString()) + ")";
                DataTable data = App.makeSQL("select m.text as text, s.name as name, m.date as date" +
                    " from messages m left join users s on m.uid_sender=s.id left join users r on m.uid_reciever=r.id where m.uid_sender in " + users + " and m.uid_reciever in " + users + ";");
                //MessageBox.Show(data.Rows.Count.ToString());
                messagesList.ItemsSource = data.DefaultView;
            }
        }
        private void updateUsers()
        {
            string sql = "select distinct u.id, u.name from users u " +
                "left join messages sen on u.id=sen.uid_sender " +
                "left join messages rec on u.id=rec.uid_reciever " +
                "where sen.uid_reciever=" + App.user["id"].ToString() +
                " or rec.uid_sender=" + App.user["id"].ToString() + ";";
            
            DataTable data = App.makeSQL(sql);
            //MessageBox.Show(data.Rows.Count.ToString());
            userList.ItemsSource = data.DefaultView;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageBox.Text) || messageBox.Text== "Введите сообщение...")
            {
                this.Close();
            } else
            {
                if(MessageBox.Show("У вас осталось не отправленное сообщение", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // check msg for SQLINJECTION!! TODO: SQLINJ
            if (!App.logged_in)
            {
                view.login loginView = new view.login();
                this.Hide();
                loginView.ShowDialog();
                this.Show();
            }
            else
            {
                
                App.makeSQL("insert into messages (uid_sender, uid_reciever, text) values " +
                "(" + App.user["id"] + ", " + reciever["id"] + ", N'" + messageBox.Text + "');");
                updateMessages();
                updateUsers();
                messageBox.Text = "";
            }
        }

        private void messageBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Введите сообщение...")
            {
                (sender as TextBox).Text = "";
            }
        }

        private void messageBox_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace((sender as TextBox).Text))
                (sender as TextBox).Text = "Введите сообщение...";
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reciever = App.makeSQL("select * from users where id=" + (((DataRowView)userList.SelectedItem).Row["id"].ToString()) + ";").Rows[0];
            messageBox.IsEnabled = true;
            sendButton.IsEnabled = true;
            updateMessages();
        }
    }
}   