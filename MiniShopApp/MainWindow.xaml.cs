
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MiniShopApp.Models;

namespace MiniShopApp
{
    public partial class MainWindow : Window
    {
        //private bool isUpdatingProductList = false;
        // Liste aller verfügbaren Produkte
        private List<Product> products = new List<Product>();

        // Zentrale Warenkorb-Instanz
        private ShoppingCart shoppingCart = new ShoppingCart();

        // Liste aller Hauptkategorien
        private List<Category> categories = new List<Category>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeCategories();
            LoadSampleData();
            //ProductList.ItemsSource = products;
            CartList.ItemsSource = shoppingCart.Items;
            UpdateTotalPrice();
        }



        // Initialisiert alle Haupt- und Unterkategorien für den TreeView

        private void InitializeCategories()
        {
            CategoryTree.Items.Clear();

            // 1. E-Zigaretten
            var eZigarettenItem = new TreeViewItem { Header = "E‑Zigaretten" };
            eZigarettenItem.Items.Add(new TreeViewItem { Header = "Einweg‑Vapes" });
            eZigarettenItem.Items.Add(new TreeViewItem { Header = "Pod‑Systeme" });
            eZigarettenItem.Items.Add(new TreeViewItem { Header = "Geräte" });
            //eZigarettenItem.IsExpanded = true;
            // 2. E-Liquids
            var eLiquidsItem = new TreeViewItem { Header = "E‑Liquids" };
            eLiquidsItem.Items.Add(new TreeViewItem { Header = "Nikotinsalz" });
            eLiquidsItem.Items.Add(new TreeViewItem { Header = "Aromen" });
            //eLiquidsItem.IsExpanded = true;
            // 3. Zubehör
            var zubehoerItem = new TreeViewItem { Header = "Zubehör" };
            zubehoerItem.Items.Add(new TreeViewItem { Header = "Pods" });
            zubehoerItem.Items.Add(new TreeViewItem { Header = "Akkus" });
            zubehoerItem.Items.Add(new TreeViewItem { Header = "Coils" });
           // zubehoerItem.IsExpanded = true;
            // 4. Snacks & Drinks
            var snacksItem = new TreeViewItem { Header = "Snacks & Drinks" };
            snacksItem.Items.Add(new TreeViewItem { Header = "Getränke" });
            snacksItem.Items.Add(new TreeViewItem { Header = "Internationale Snacks" });
           // snacksItem.IsExpanded = true;
            // Ana kategorileri TreeView'e ekle
            CategoryTree.Items.Add(eZigarettenItem);
            CategoryTree.Items.Add(eLiquidsItem);
            CategoryTree.Items.Add(zubehoerItem);
            CategoryTree.Items.Add(snacksItem);
        }


        private Dictionary<string, List<string>> categoryMap = new()
{
    { "E‑Zigaretten", new List<string> { "Einweg‑Vapes", "Pod‑Systeme", "Geräte" } },
    { "E‑Liquids", new List<string> { "Nikotinsalz", "Aromen" } },
    { "Zubehör", new List<string> { "Pods", "Akkus", "Coils" } },
    { "Snacks & Drinks", new List<string> { "Getränke", "Internationale Snacks" } }
};


        // Lädt Beispieldaten für Produkte


        private void LoadSampleData()
        {
            products.Clear();


            products.Add(new Product
            {
                Id = 1,
                Name = "Disposable Vape 500 Puffs",
                Price = 7.99,
                Stock = 50,
                CategoryName = "Einweg‑Vapes"
            });

            products.Add(new Product
            {
                Id = 2,
                Name = "Disposable Vape Mint",
                Price = 8.49,
                Stock = 40,
                CategoryName = "Einweg‑Vapes"
            });


            products.Add(new Product
            {
                Id = 3,
                Name = "Snack Pack",
                Price = 9.99,
                Stock = 20,
                CategoryName = "Internationale Snacks"
            });


            products.Add(new Product
            {
                Id = 4,
                Name = "Apple Nikotinsalz",
                Price = 5.99,
                Stock = 30,
                CategoryName = "Nikotinsalz"
            });


            products.Add(new Product
            {
                Id = 5,
                Name = "Sweet Snack Box",
                Price = 3.49,
                Stock = 15,
                CategoryName = "Internationale Snacks"
            });
            products.Add(new Product
            {
                Id = 6,
                Name = "Replacement Pods (2‑Pack)",
                Price = 4.99,
                Stock = 30,
                CategoryName = "Pods"
            });

            products.Add(new Product
            {
                Id = 7,
                Name = "18650 Akku",
                Price = 6.49,
                Stock = 20,
                CategoryName = "Akkus"
            });

            products.Add(new Product
            {
                Id = 8,
                Name = "Vape Coil 0.8 Ohm",
                Price = 3.99,
                Stock = 40,
                CategoryName = "Coils"
            });


            products.Add(new Product
            {
                Id = 9,
                Name = "Berry Mix Nikotinsalz",
                Price = 6.49,
                Stock = 25,
                CategoryName = "Nikotinsalz"
            });




        }

        // Fügt das ausgewählte Produkt dem Warenkorb hinzu

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.Tag is not Product selectedProduct)
                return;

            var existingItem = shoppingCart.Items
                .FirstOrDefault(item => item.Product.Id == selectedProduct.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                shoppingCart.Items.Add(new CartItem
                {
                    Product = selectedProduct,
                    Quantity = 1
                });
            }

            RefreshCart();
        }


        // Entfernt die ausgewählte Position aus dem Warenkorb
        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = CartList.SelectedItem as CartItem;
            if (selectedItem == null) return;

            shoppingCart.Items.Remove(selectedItem);
            RefreshCart();
        }

        private void CategoryTree_SelectedItemChanged(
     object sender,
     RoutedPropertyChangedEventArgs<object> e)
        {
            if (CategoryTree.SelectedItem is not TreeViewItem item)
                return;

            string selected = item.Header.ToString();

            // ✅ ANA KATEGORİ SEÇİLDİYSE
            if (categoryMap.ContainsKey(selected))
            {
                var subCategories = categoryMap[selected];

                ProductList.ItemsSource = products
                    .Where(p => subCategories.Contains(p.CategoryName))
                    .ToList();

                return;
            }

            // ✅ ALT KATEGORİ SEÇİLDİYSE
            ProductList.ItemsSource = products
                .Where(p => p.CategoryName == selected)
                .ToList();
        }





        // Aktualisiert die Warenkorbansicht
        private void RefreshCart()
        {
            CartList.ItemsSource = null;
            CartList.ItemsSource = shoppingCart.Items;
            UpdateTotalPrice();
        }

        // Aktualisiert die Anzeige des Gesamtpreises
        private void UpdateTotalPrice()
        {
            TotalText.Text = $"Total: {shoppingCart.TotalPrice:F2} €";
        }
    }
}
