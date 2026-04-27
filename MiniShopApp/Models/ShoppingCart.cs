using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShopApp.Models
{
    public class ShoppingCart
    {
        // Liste aller Positionen im Warenkorb
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Berechnet den Gesamtpreis aller Warenkorbpositionen
        public double TotalPrice
        {
            get
            {
                return Items.Sum(item => item.Product.Price * item.Quantity);
            }
        }
    }
}
