using System;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace OOP_5_3
{
    public class DB
    {
        public string myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=;database=shop";
        MySqlConnection conn = new MySqlConnection();
        DataTable dataTable = new DataTable();
        string SQL= "SELECT * FROM products";
        public void connect_to_db()
        {
            conn.ConnectionString= myConnectionString;
            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        public DataTable GetAllData()
        {
            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);
            da.Dispose();
            return dataTable;
        }
    }
}
