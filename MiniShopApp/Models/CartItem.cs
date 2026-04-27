using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShopApp.Models
{
    public class CartItem
    {
        // Referenz auf das Produkt im Warenkorb
        public Product Product { get; set; }

        // Anzahl des Produkts im Warenkorb
        public int Quantity { get; set; }
    }
}
