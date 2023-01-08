

using BlApi;
using System.Windows;
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty IdProperty = DependencyProperty.Register(nameof(OrderId),typeof(int?), typeof(OrderTrackingWindow));

    public int? OrderId
    {
        get => (int?)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public static readonly DependencyProperty OrderProperty = DependencyProperty.Register(nameof(Order), typeof(BO.OrderTracking), typeof(OrderTrackingWindow));

    public BO.OrderTracking? Order
    {
        get => (BO.OrderTracking)GetValue(OrderProperty);
        set => SetValue(OrderProperty, value);
    }

    public OrderTrackingWindow()
    {
        InitializeComponent();
    }

    private void GetOrderStatus(object sender, RoutedEventArgs e)
    {
        try
        {
            Order = bl?.Order.TrackOrder(OrderId ?? -1);
        }
        catch
        {
            MessageBox.Show("We are sorry, we couldn't find an order with id: " + OrderId);
        }
    }

    private void ShowOrderDetails(object sender, RoutedEventArgs e)
    {
        new OrderWindow(OrderId ?? -1, State.View).Show();
    }
}
