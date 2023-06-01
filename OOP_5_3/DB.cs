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
        public static string myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=;database=shop";
        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        public BitmapImage bi;
        MySqlDataReader reader;
        string SQL= "SELECT * FROM products";
        byte[] rawData;
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

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            
            return image;
        }

        public void generateImage()
        {
            cmd = new MySqlCommand(SQL, conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            long len = reader.GetBytes(reader.GetOrdinal("image"), 0, null, 0, 0);
            rawData = new byte[len];
            len = reader.GetBytes(3, 0, rawData, 0, (int)len);
            bi = LoadImage(rawData);
            reader.Close();
        }

    }
}
