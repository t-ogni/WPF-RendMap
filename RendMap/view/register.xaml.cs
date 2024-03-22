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
    /// Логика взаимодействия для register.xaml
    /// </summary>
    public partial class register : Window
    {
        public register()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
