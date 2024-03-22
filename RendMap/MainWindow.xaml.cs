using RendMap.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RendMap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataTable ads = App.makeSQL("select * from ads;");
            Ads.ItemsSource = ads.DefaultView;
            updateRights();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            updateRights();
            if (App.logged_in)
            {
                if (MessageBox.Show("Вы хотите выйти?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
                {
                    App.logged_in = false;
                    updateRights();
                    LoginButton.Content = "Вход не выполнен. Войти";
                }
            }
            else
            {
                view.login loginView = new view.login();
                this.Hide();
                loginView.ShowDialog();
                this.Show();
                updateRights();

                if (App.logged_in)
                    LoginButton.Content = "Вошли под \"" + App.user["name"] + "\". Выйти";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заглушка");
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            view.messenger messenger = new view.messenger();
            messenger.Show();
        }

        private void Ad_Selected(object sender, RoutedEventArgs e)
        {
            view.adView ad = new view.adView((DataRowView)Ads.SelectedItem);
            ad.Show();

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            view.user user = new view.user();
            this.Hide();
            user.ShowDialog();
            this.Show();
        }

        public void updateRights()
        {
            if (App.logged_in) {
                Chat.Visibility = Visibility.Visible;
                NewAd.Visibility = Visibility.Visible;
                if ((bool)App.user["moderator"] == true)
                {
                    Moder.Visibility = Visibility.Visible;
                } else
                {
                    Moder.Visibility = Visibility.Collapsed;
                }
            } else
            {
                Chat.Visibility = Visibility.Collapsed;
                NewAd.Visibility = Visibility.Collapsed;
                Moder.Visibility = Visibility.Collapsed;
            }
            
        }

            private void Ads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool Expanded = true;

        private void FilterOpenClose(object sender, RoutedEventArgs e)
        {
            if (Expanded)
            {
                var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
                anim.Completed += (s, _) => Expanded = false;
                ToolBox.BeginAnimation(ContentControl.WidthProperty, anim);
                FilterArrow.ScaleX = -1;
            }
            else
            {
                var anim = new DoubleAnimation(150, (Duration)TimeSpan.FromSeconds(0.3));
                anim.Completed += (s, _) => Expanded = true;
                ToolBox.BeginAnimation(ContentControl.WidthProperty, anim);
                FilterArrow.ScaleX = 1;
            }
        }

        private void NewAd_Click(object sender, RoutedEventArgs e)
        {
            view.adCreate ad = new view.adCreate();
            ad.Show();
        }


        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Поиск")
            {
                (sender as TextBox).Text = "";
            }
        }

        private void regenerateList()
        {
            string sqlQuery = "select * from ads where ";
            string select = ((ComboBoxItem)IntervalCombo.SelectedItem).Tag.ToString();
            if (select == "daily")
                sqlQuery += "is_arenda=1 and is_daily=1 ";
            else if (select == "monthly")
                sqlQuery += "is_arenda=1 and is_daily=0 ";
            else if (select == "buy")
                sqlQuery += "is_arenda=0 and is_daily=0 ";
            else
                sqlQuery += "is_daily in (0,1) ";

            ;



            if (search.Text != "Поиск")
            {
                sqlQuery += "and (title like N'%" + search.Text.ToString() + "%' or text like N'%" + search.Text.ToString() + "%') ";
            }

            sqlQuery += ";";

            DataTable ads = App.makeSQL(sqlQuery);
            Ads.ItemsSource = ads.DefaultView;
        }

        private void seacrh_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace((sender as TextBox).Text))
                (sender as TextBox).Text = "Поиск";
        }


        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text != "Поиск")
                regenerateList();
        }


        private void search_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
        }

        private void Moder_Click(object sender, RoutedEventArgs e)
        {
            view.AdminPanel adm = new view.AdminPanel();
            adm.Show();
        }

        private void IntervalCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            regenerateList();
        }
    }
}
