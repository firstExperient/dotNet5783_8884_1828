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

        public static readonly DependencyProperty CartProperty
      = DependencyProperty.Register(nameof(Cart), typeof(BO.Cart), typeof(CatalogWindow));

        public BO.Cart Cart
        {
            get => (BO.Cart)GetValue(CartProperty);
            set => SetValue(CartProperty, value);
        }

        public CatalogWindow()
        {
            Cart = new BO.Cart();
            ProductsList = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(Cart));
            InitializeComponent();
        }
        public CatalogWindow(Cart cart)
        {
            Cart = cart;
            ProductsList = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(Cart));
            InitializeComponent();
        }

        /// <summary>
        /// This fuction opens the cart window
        /// </summary>
        private void ShowCart_click(object sender, RoutedEventArgs e)
        {
            new CartView().Show();
            //Close();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.ProductItem)
            {
                //new ProductWindow((element.DataContext as BO.ProductItem)!.ID).ShowDialog();
                //ProductsList = new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(Cart)!);
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
    }
}
