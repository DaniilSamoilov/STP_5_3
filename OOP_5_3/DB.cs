using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace OOP_5_3
{
    public interface DB_inreface
    {
        void connect_to_db();
        

    }
    public class DB:DB_inreface
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader mysql_request;
        MySqlDataAdapter data_adapter;
        DataTable table = new DataTable();
        MySqlCommandBuilder command_builder;
        public string myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=;database=shop";
        byte[] raw;
        public void connect_to_db()
        {
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                cmd = conn.CreateCommand();
                conn.Open();
                MessageBox.Show("Connected");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string get_some_data()
        {
            cmd.CommandText = "SELECT * FROM products;";
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
            return "1";
        }
        public void get_image()
        {

        }
    }
}
