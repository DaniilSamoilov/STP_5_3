using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace OOP_5_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        DB myDB = new DB();
        internal cart myCart;
        List<product_card> product_cards = new List<product_card>();
        DataTable data;
        public MainWindow()
        {   
            InitializeComponent();
            myCart = new cart(totalSum);
            myDB.connect_to_db();
            data = myDB.GetAllData();
            foreach(DataRow dr in data.Rows)
            {
                var productCard = new product_card(myCart);
                product_cards.Add(productCard);
                products_panel.Children.Add(productCard.generateCard(dr));
            }
        }

        private void cart_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
