using System;
using System.Collections.Generic;
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

        public Grid generate_cartd()
        {
            var MyGrid = new Grid();
            SetGrid(MyGrid);
            var MyWrapPanel = new WrapPanel();
            var ProductName = new Label();
            SetNameLabel(ProductName);
            MyGrid.Children.Add(MyWrapPanel);
            return MyGrid;
        }

        public void SetGrid(Grid grid)
        {
            grid.MaxWidth= 300;
            grid.MinWidth= 200;
        }
        public void SetNameLabel(Label label)
        {
            label.Content= this.Name;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        }
    }
}
