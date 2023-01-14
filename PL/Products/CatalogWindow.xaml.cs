using BO;
using System.Collections.ObjectModel;
using System.Windows;
using BlApi;
using PL.Products;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using System.Windows.Controls;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window,INotifyPropertyChanged
    {
        private IBl? bl = BlApi.Factory.Get();

        public event PropertyChangedEventHandler? PropertyChanged;

        private IEnumerable<BO.ProductItem?>? productsList;

        public IEnumerable<BO.ProductItem?>? ProductsList
        {
            get => productsList;
            set
            {
                productsList = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ProductsList"));
            }
        }

        private IEnumerable<IGrouping<BO.Category?, BO.ProductItem>> FullProductsList;

        public static IEnumerable Categories = Enum.GetValues(typeof(BO.Category));

        public BO.Cart Cart;
        
        public CatalogWindow()
        {
            Cart = new BO.Cart();
            FullProductsList = bl.Product.GetCatalog(Cart);
            ProductsList = FullProductsList.SelectMany(x=>x.AsEnumerable());
            InitializeComponent();
        }

        public CatalogWindow(BO.Cart cart)
        {
            Cart = cart;
            FullProductsList = bl.Product.GetCatalog(Cart);
            ProductsList = FullProductsList.SelectMany(x => x.AsEnumerable());
            InitializeComponent();
        }

        /// <summary>
        /// This function addes a product to the user's cart
        /// </summary>
        private void AddToCart(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.ProductItem)
            {
                var product = (BO.ProductItem)element.DataContext;
                try
                {
                    Cart = bl!.Cart.AddItem(product.ID, Cart);
                   // new CartWindow(Cart).Show();
                   // Close();
                }
                catch (BO.OutOfStockException)
                {
                    MessageBox.Show($"we are sorry, {product.Name} is out of stock");
                }
           }
        }

        /// <summary>
        /// This function open a window of the product's details
        /// </summary>
        private void ShowProductItem(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.ProductItem)
            {
                new ProductItemWindow((element.DataContext as BO.ProductItem)!,Cart).Show();
                Close();
            }
        }

        /// <summary>
        /// This function opens a window of the user's cart
        /// </summary>
        private void ShowCart(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart).Show();
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var element = args.OriginalSource as ComboBox;
            if (element != null)
            {
                ProductsList = FullProductsList.Where(x=>x.FirstOrDefault()?.Category == (BO.Category)element.SelectedItem).FirstOrDefault()?.AsEnumerable();
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}