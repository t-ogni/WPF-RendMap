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

namespace RendMap.view
{
    /// <summary>
    /// Логика взаимодействия для adCreate.xaml
    /// </summary>
    public partial class adCreate : Window
    {
        public adCreate()
        {
            InitializeComponent();
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(titleBox.Text))
            {
                titleWarn.Text = "Необходимо заполнить";
                titleWarn.Visibility = Visibility.Visible;
            }
            else
                titleWarn.Visibility = Visibility.Hidden;
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textWarn.Text = "Необходимо заполнить";
                textWarn.Visibility = Visibility.Visible;
            }
            else
                textWarn.Visibility = Visibility.Hidden;
        }

        private void Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(valueBox.Text) || Convert.ToInt32(valueBox.Text) == 0)
            {
                valueWarn.Text = "Необходимо заполнить";
                valueWarn.Visibility = Visibility.Visible;
            }
            else
                valueWarn.Visibility = Visibility.Hidden;
        }

        private void CreateAd_Click(object sender, RoutedEventArgs e)
        {
            bool to_return = false;

            if (string.IsNullOrWhiteSpace(titleBox.Text)) {
                titleWarn.Text = "Необходимо заполнить";
                titleWarn.Visibility = Visibility.Visible;
                to_return = true;
            }
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textWarn.Text = "Необходимо заполнить";
                textWarn.Visibility = Visibility.Visible;
                to_return = true;
            }
            if (string.IsNullOrWhiteSpace(valueBox.Text) || Convert.ToInt32(valueBox.Text) ==0)
            {
                valueWarn.Text = "Необходимо заполнить";
                valueWarn.Visibility = Visibility.Visible;
                to_return = true;
            }
            if (to_return)
                return;

            string sqlQuery = "insert into ads (uid, title, text,value) values (";
            sqlQuery += App.user["id"] + ", ";
            sqlQuery += "N'"+titleBox.Text + "', ";
            sqlQuery += "N'"+textBox.Text + "', "; ;
            sqlQuery += valueBox.Text + ");";
            App.makeSQL(sqlQuery);
            this.Close();
        }
        private void Image_add(object sender, RoutedEventArgs e)
        {
            
        }

        private void Check_changed(object sender, RoutedEventArgs e)
        {
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Закрыть?", "Уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            this.Close();
        }

        private void IntervalCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string select = ((ComboBoxItem)IntervalCombo.SelectedItem).Tag.ToString();
            if (select == "daily")
                unit.Text = "руб / день";
            else if (select == "monthly")
                unit.Text = "руб / мес";
            else
                unit.Text = "руб.";
        }
    }
}
