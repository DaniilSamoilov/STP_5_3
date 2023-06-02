using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOP_5_3
{
    public interface Icart{
        float TotalPrice { get; }
    }
    internal class cart:Icart
    {
        public float TotalPrice { get; private set; }
        private Dictionary<int,product_card> products = new Dictionary<int, product_card>();
        private Dictionary<int, int> AmoutDict = new Dictionary<int, int>();
        System.Windows.Controls.Label priceDisplay;
        public cart(System.Windows.Controls.Label label)
        {
            this.priceDisplay = label;
        }

        public void AddProduct(product_card card)
        {
            if (!products.ContainsKey(card.id))
            {
                products.Add(card.id,card);
                AmoutDict.Add(card.id, 1);
            }
            else
            {
                AmoutDict[card.id]++;
            }
            
            CountSum(card.price);
        }
        public void RemoveProduct(int id) 
        {
            if (AmoutDict.ContainsKey(id))
            {
                CountSum(-products[id].price);
                AmoutDict[id] -= 1;
                if (AmoutDict[id] == 0)
                {
                    AmoutDict.Remove(id);
                    products.Remove(id);
                }
            }
        }
        public void CountSum(float price)
        {
            TotalPrice+= price;
            this.priceDisplay.Content = TotalPrice.ToString();
        }
        public void Pay()
        {
            TotalPrice = 0;
            products.Clear();
            AmoutDict.Clear();
        }
    }   
}
