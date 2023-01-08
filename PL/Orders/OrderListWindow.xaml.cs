
using PL.Products;
using System.Collections.Generic;
using System.Windows;
using BlApi;
using System.Linq;
using System.Collections.ObjectModel;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ListProperty
        = DependencyProperty.Register(nameof(OrdersList), typeof(ObservableCollection<BO.OrderForList?>), typeof(OrderListWindow));

    public ObservableCollection<BO.OrderForList?> OrdersList
    {
        get => (ObservableCollection<BO.OrderForList?>)GetValue(ListProperty);
        set => SetValue(ListProperty, value);
    }

    public OrderListWindow()
    {
        OrdersList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetAll());
        InitializeComponent();
    }

    private void UpdateOrder(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderForList)
        {
            new OrderWindow((element.DataContext as BO.OrderForList)!.ID,State.Update).ShowDialog();
            OrdersList = new ObservableCollection<BO.OrderForList?>(bl?.Order.GetAll()!);
        }
    }

    private void ShipOrder(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderForList)
        {
            //int orderId = (element.DataContext as BO.OrderForList)!.ID;

            ////OrdersList.ElementAt(index).st //Select(x=>x.ID==orderId ? )
            ////= 5;
            ////int index = OrdersList!.ToList().FindIndex(order => order?.ID == orderId);
            //(element.DataContext as BO.OrderForList)!.Status = bl?.Order.ShipOrder(orderId).Status;

            int orderId = (element.DataContext as BO.OrderForList)!.ID;
            int index = OrdersList!.ToList().FindIndex(order => order?.ID == orderId);
            OrdersList!.ElementAt(index)!.Status = bl?.Order.ShipOrder(orderId).Status;
        }
    }

    private void DeliverOrder(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderForList)
        {
            int orderId = (element.DataContext as BO.OrderForList)!.ID;
            int index = OrdersList!.ToList().FindIndex(order => order?.ID == orderId);
            //OrdersList!.ElementAt(index)!.Status = bl?.Order.DeliverOrder(orderId).Status;
            OrdersList[index].Status = bl?.Order.DeliverOrder(orderId).Status;
        }
    }

}
