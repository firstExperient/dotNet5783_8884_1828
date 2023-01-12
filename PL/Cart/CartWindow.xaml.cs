
using BlApi;
using System.ComponentModel;
using System.Windows;

namespace PL.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class CartWindow : Window,INotifyPropertyChanged
{

    private IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private BO.Cart? cart;
    public BO.Cart? Cart
    {
        get { return cart; }
        set { 
            cart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
        }
    }

    public CartWindow(BO.Cart cart)
    {
        Cart = cart;
        InitializeComponent();
    }


    private void ShowCatalog(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(Cart!).Show();
        Close();
    }

    private void AddOne(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderItem)
        {
            var orderItem = (BO.OrderItem)element.DataContext;
            try
            {
                Cart = bl!.Cart.UpdateItemAmount(orderItem.ProductId, Cart!, orderItem.Amount + 1);
            }
            catch (BO.OutOfStockException)
            {
                MessageBox.Show($"we are sorry, {orderItem.Name} is out of stock");
            }
        }
    }



    private void DecreaseOne(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderItem)
        {
            var orderItem = (BO.OrderItem)element.DataContext;
            try
            {
                Cart = bl!.Cart.UpdateItemAmount(orderItem.ProductId, Cart!, orderItem.Amount - 1);
            }
            catch (BO.OutOfStockException)
            {
                MessageBox.Show($"we are sorry, {orderItem.Name} is out of stock");
            }
        }
    }
    private void RemoveItem(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderItem)
        {
            var orderItem = (BO.OrderItem)element.DataContext;
            try
            {
                Cart = bl!.Cart.UpdateItemAmount(orderItem.ProductId, Cart!, 0);
            }
            catch (BO.OutOfStockException)
            {
                MessageBox.Show($"we are sorry, {orderItem.Name} is out of stock");
            }
        }
    }
}
