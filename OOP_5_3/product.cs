using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OOP_5_3
{
    public interface global_product { 
        string Name { get; set; }
        float Price { get; set; }
    }

    public class product_card
    {
        private string Name { get; set; }
        private float Price { get; set; }
        private Image image { get; set; }

        public Grid generate_cartd(DataRow dr)
        {
            var MyGrid = new Grid();
            SetGrid(MyGrid);
            var MyWrapPanel = new WrapPanel();
            var ProductName = new Label();
            SetNameLabel(ProductName, dr["Name"].ToString());
            MyGrid.Children.Add(MyWrapPanel);
            return MyGrid;
        }

        public void SetGrid(Grid grid)
        {
            grid.MaxWidth= 300;
            grid.MinWidth= 200;
        }
        public void SetNameLabel(Label label,string Name)
        {
            label.Content= Name;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
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
        public Image generateImage(byte[] rawData)
        {
            BitmapImage bi = generate_bitmap_from_bytes(rawData);
            Image img = new Image();
            img.Source = bi;
            return img;
        }
    }
}
