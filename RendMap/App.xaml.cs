using RendMap.view;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RendMap
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["RendMap.dbConnectionString"].ConnectionString;
        public static SqlDataAdapter adapter;
        public static SqlConnection connection = null;
        public static string loginRegex = @"^[a-zA-Z0-9]*$";

        public static bool logged_in = false;
        public static string Username = "";
        public static DataRow user = null;

        public static DataTable makeSQL(string request) {

            DataTable Table = new DataTable();
            try
            {
                if (connection is null || connection.State != ConnectionState.Open)
                    connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(request, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(Table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return Table;
        }
    }
}
