using BO;
using System.Collections.ObjectModel;
using System.Windows;
using BlApi;
using PL.Products;
using PL.Orders;

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty ListProperty
      = DependencyProperty.Register(nameof(ProductItem), typeof(ObservableCollection<BO.ProductItem?>), typeof(CatalogWindow));

        public ObservableCollection<BO.ProductItem?> ProductsList
        {
            get => (ObservableCollection<BO.ProductItem?>)GetValue(ListProperty);
            set => SetValue(ListProperty, value);
        }


        public BO.Cart Cart;
        

        public CatalogWindow()
        {
            Cart = new BO.Cart();
            ProductsList = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(Cart));
            InitializeComponent();
        }
        public CatalogWindow(BO.Cart cart)
        {
            Cart = cart;
            ProductsList = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(Cart));
            InitializeComponent();
        }

        

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.ProductItem)
            {
                var product = (BO.ProductItem)element.DataContext;
                try
                {
                    Cart = bl!.Cart.AddItem(product.ID, Cart);
                    ProductsList[ProductsList.IndexOf(product)]!.Amount = product.Amount + 1;
                    new CartWindow(Cart).Show();
                    Close();
                }
                catch (BO.OutOfStockException)
                {
                    MessageBox.Show($"we are sorry, {product.Name} is out of stock");
                }
           }
        }

        private void ShowProductItem(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.ProductItem)
            {
                new ProductItemWindow((element.DataContext as BO.ProductItem)!,Cart).Show();
                Close();
            }
        }

        private void ShowCart(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart).Show();
            Close();
        }

    }
}
