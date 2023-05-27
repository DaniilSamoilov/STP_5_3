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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace OOP_5_3
{

    public class DB
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader mysql_request;
        public string myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=;database=shop";

        public void connect_to_db()
        {
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM products;";
                conn.Open();
                mysql_request = cmd.ExecuteReader();
                while (mysql_request.Read())
                {
                    string respond = "";
                    for (int i = 0; i < mysql_request.FieldCount; i++)
                    {
                        if (!mysql_request.IsDBNull(i))
                        {
                            respond += mysql_request.GetString(i);
                        }
                    }
                    MessageBox.Show(respond);
                }
                MessageBox.Show("Connected");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string get_some_data()
        {
            return "1";
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB myDB = new DB();
        public MainWindow()
        {
            
            InitializeComponent();
            
            myDB.connect_to_db();
        }
    }
}
