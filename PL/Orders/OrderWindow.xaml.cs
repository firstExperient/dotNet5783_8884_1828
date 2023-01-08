using BlApi;
using PL.Products;
using System.Windows;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(State), typeof(ProductWindow));
    public State State
    {
        get => (State)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public static readonly DependencyProperty OrderProperty = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderWindow));

    public BO.Order Order
    {
        get { return (BO.Order)GetValue(OrderProperty); }
        set { SetValue(OrderProperty, value); }
    }
    public OrderWindow(int orderId, State state)
    {
        State = state;
        Order = bl.Order.Get(orderId);
        InitializeComponent();
    }

    private void Remove_Item(object sender, RoutedEventArgs e)
    {
        //var element = e.OriginalSource as FrameworkElement;
        //if (element != null && element.DataContext is BO.OrderItem)
        //{
            
        //    new OrderWindow((element.DataContext as BO.OrderForList)!.ID).ShowDialog();
        //    OrdersList = bl?.Order.GetAll();
        //}
        //(.DataContext as BO.OrderItem).ID
    }
    private void ShipOrder(object sender, RoutedEventArgs e)
    {
        Order = bl?.Order.ShipOrder(Order.ID)!;
    }

    private void DeliverOrder(object sender, RoutedEventArgs e)
    {
        Order = bl?.Order.DeliverOrder(Order.ID)!; 
    }
}
