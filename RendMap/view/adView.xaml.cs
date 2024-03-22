using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace RendMap.view
{
    /// <summary>
    /// Логика взаимодействия для adView.xaml
    /// </summary>
    public partial class adView : Window
    {
        private DataRowView ad;
        public adView(DataRowView ad)
        {
            this.ad = ad;

            this.Title = ad.Row["title"].ToString();
            InitializeComponent();
            try
            {
                picture.Source = (ImageSource)FindResource(ad.Row["pic"].ToString());
            }
            catch {
            }
            title.Text = ad.Row["title"].ToString();
            userBox.Text = App.makeSQL("select * from users where id="+ad.Row["uid"].ToString()+";").Rows[0]["name"].ToString();
            textBox.Text = ad.Row["text"].ToString();
            valueBox.Text = (ad.Row["value"]).ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Deal_Click(object sender, RoutedEventArgs e)
        {
            
            view.deal deal = new view.deal();
            deal.Show();
            this.Close();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            
            view.messenger messenger = new view.messenger((int) ad.Row["uid"]);
            messenger.Show();
            this.Close();
        }
    }
}
