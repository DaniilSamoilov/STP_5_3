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
        public void GetAllData()
        {
            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);
        }
        private static BitmapImage generate_bitmap_from_bytes(byte[] imageData)
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

        public Image generateImage()
        {
            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            long len = reader.GetBytes(reader.GetOrdinal("image"), 0, null, 0, 0);
            byte[] rawData = new byte[len];
            len = reader.GetBytes(3, 0, rawData, 0, (int)len);
            BitmapImage bi = generate_bitmap_from_bytes(rawData);
            reader.Close();
            Image img = new Image();
            img.Source = bi;
            return img;
        }

        public Grid generate_product_card()
        {
            Grid mygrid = new Grid();
            Image product_image = generateImage();
            setImageParams(product_image);
            Label product_label = new Label();
            Label product_price = new Label();
            Button add_to_cart_btn = new Button();
            setCartButtonParams(add_to_cart_btn);
            product_price.Content = 123;
            product_label.Content = "что-то";
            mygrid.Children.Add(product_image);
            mygrid.Children.Add(product_label);
            mygrid.Children.Add(product_price);
            mygrid.Children.Add(add_to_cart_btn);
            return mygrid;
        }

        private void setCartButtonParams(Button add_to_cart_btn)
        {
            add_to_cart_btn.Content = "Добавить в корзину";
            add_to_cart_btn.Width = 80;
            add_to_cart_btn.Height = 40;
            add_to_cart_btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(button1_Click));
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void setImageParams(Image product_image)
        {
            product_image.Width = 200;
            product_image.Height = 200;

        }
    }
}
