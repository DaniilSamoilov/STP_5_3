using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OOP_5_3
{
    public interface global_product
    {
        string Name { get; }
        float price { get; }
        int id { get; }
    }
    internal class product_card:global_product
    {
        public string Name {  get; private set; }
        public float price { get; private set; }
        public int id { get; private set; }
        private cart myCart;
        public product_card(cart myCart)
        { 
            this.myCart = myCart; 
        }

        public Grid generateCard(DataRow dr)
        {
            var MyGrid = new Grid();
            SetGrid(MyGrid);

            this.id = (int)dr["id"];

            var MyWrapPanel = new WrapPanel();

            var ProductName = new Label();
            SetLabel(ProductName, dr["Name"].ToString());
            this.Name = dr["Name"].ToString()   ;
            MyWrapPanel.Children.Add(ProductName);

            var image = generateImage((byte[])dr["image"]);
            SetImage(image);
            MyWrapPanel.Children.Add(image);

            var price = new Label();
            SetLabel(price, "Цена:"+ dr["price"].ToString()+"Р");
            this.price = (int)dr["price"];
            MyWrapPanel.Children.Add(price);

            var addToCartBtn = new Button();
            SetBtn(addToCartBtn, "Добавить в корзину",true);
            MyWrapPanel.Children.Add(addToCartBtn);

            var remooveFromCartBtn = new Button();
            SetBtn(remooveFromCartBtn,"Убрать из корзины",false);
            MyWrapPanel.Children.Add(remooveFromCartBtn);

            MyGrid.Children.Add(MyWrapPanel);
            return MyGrid;
        }

        private void SetBtn(Button btn,string text,bool Add)
        {
            btn.MaxWidth=200;
            btn.MinWidth = 100;
            btn.MaxHeight= 100;
            btn.MinHeight = 50;
            btn.Content = text;
            if (Add)
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(AddBtnEvent));
            }
            else { 
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(RemoveBtnEvent));
            }
        }
        public void AddBtnEvent(object sender, RoutedEventArgs e)
        {
            myCart.AddProduct(this);
        }
        public void RemoveBtnEvent(object sender, RoutedEventArgs e)
        {
            myCart.RemoveProduct(this.id);
        }
        private void SetImage(Image image)
        {
            image.MaxHeight = 400;
            image.MinHeight= 200;
            image.MaxWidth = 300;
            image.MinWidth = 200;
        }
        private void SetGrid(Grid grid)
        {
            grid.MaxWidth = 300;
            grid.MinWidth = 200;
            grid.MaxHeight = 500;
            grid.MinHeight = 400;
        }
        private void SetLabel(Label label,string Name)
        {
            label.Content= Name;
            label.MaxWidth = 300;
            label.MinWidth = 200;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
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
        private Image generateImage(byte[] rawData)
        {
            BitmapImage bi = generate_bitmap_from_bytes(rawData);
            Image img = new Image();
            img.Source = bi;
            return img;
        }
    }
}
