
using BlApi;
using BO;
using System.ComponentModel;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for FinishOrderWindow.xaml
/// </summary>
public partial class FinishOrderWindow : Window,INotifyPropertyChanged
{
    private IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private BO.Cart cart;
    public BO.Cart Cart
    {
        get { return cart; }
        set
        {
            cart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
        }
    }

    public FinishOrderWindow(BO.Cart cart)
    {
        Cart = cart;
        InitializeComponent();
    }

    private void MakeOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            int? orderId = bl?.Cart.ConfirmOrder(Cart);
            if (orderId != null)
            {
                new SuccessOrderWindow((int)orderId!).Show();
                Close();
            }
        }
        catch
        {
            MessageBox.Show("ooops, something went worng");
        }
    }
}
