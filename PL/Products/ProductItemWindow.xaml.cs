
using BlApi;
using System.Windows;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductView.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ProductProperty = DependencyProperty.Register(nameof(Product),typeof(BO.ProductItem),typeof(ProductItemWindow));


    public BO.ProductItem Product
    {
        get { return (BO.ProductItem)GetValue(ProductProperty); }
        set { SetValue(ProductProperty, value); }
    }

    public BO.Cart Cart;
   
    public ProductItemWindow(BO.ProductItem product, BO.Cart cart)
    {
        Product = product;
        Cart = cart;
        InitializeComponent();
    }

    private void AddToCart(object sender, RoutedEventArgs e)
    {
        Cart = bl!.Cart.AddItem(Product.ID, Cart);
        Product = bl.Product.Get(Product.ID, Cart);
    }

    private void RemoveFromCart(object sender, RoutedEventArgs e)
    {
        Cart = bl!.Cart.UpdateItemAmount(Product.ID, Cart, 0);
        Product = bl.Product.Get(Product.ID, Cart);
    }

    private void ReturnToCatalog(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(Cart).Show();
        Close();
    }
}
