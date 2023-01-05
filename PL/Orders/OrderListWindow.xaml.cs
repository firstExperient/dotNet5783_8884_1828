
using PL.Products;
using System.Collections.Generic;
using System;
using System.Windows;
using BlApi;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ListProperty
        = DependencyProperty.Register(nameof(OrdersList), typeof(IEnumerable<BO.OrderForList?>), typeof(ProductListWindow));

    public IEnumerable<BO.OrderForList?>? OrdersList
    {
        get => (IEnumerable<BO.OrderForList?>)GetValue(ListProperty);
        set => SetValue(ListProperty, value);
    }

    public OrderListWindow()
    {
        OrdersList = bl.Order.GetAll();
        InitializeComponent();
    }

    private void updateOrder(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderForList)
        {
            new OrderWindow((element.DataContext as BO.OrderForList)!.ID).ShowDialog();
            OrdersList = bl?.Order.GetAll();
        }
    }
}
