using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            
            InitializeComponent();
            
            myDB.connect_to_db();
        }

        private void my_btn_Click(object sender, RoutedEventArgs e)
        {
            myDB.generateImage();
            Image img = new Image();
            img.Source = myDB.bi;
            my_grid.Children.Add(img);
        }
    }
}
