
using System.Windows;
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    public static readonly DependencyProperty IdProperty = DependencyProperty.Register(nameof(OrderId),typeof(int?), typeof(OrderTrackingWindow));

    public int? OrderId
    {
        get => (int?)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public OrderTrackingWindow()
    {
        InitializeComponent();
    }
}
