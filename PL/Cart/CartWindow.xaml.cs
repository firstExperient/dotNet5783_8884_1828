﻿
using BlApi;
using PL.Products;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    private BO.Cart cart;
    public BO.Cart Cart
    {
        get { return cart; }
        set { 
            cart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
        }
    }

    public ObservableCollection<BO.OrderItem?>? Items
    {
        get { return cart.Items; }
        set
        {
            cart.Items = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
        }
    }

    public CartWindow(BO.Cart cart)
    {
        Cart = cart;
        InitializeComponent();
    }


    private void ShowCatalog(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(Cart).Show();
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
                Cart = bl!.Cart.UpdateItemAmount(orderItem.ProductId, Cart, orderItem.Amount + 1);
                Items = Cart.Items;
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
                Cart = bl!.Cart.UpdateItemAmount(orderItem.ProductId, Cart, orderItem.Amount - 1);
                Items = Cart.Items;
            }
            catch (BO.OutOfStockException)
            {
                MessageBox.Show($"we are sorry, {orderItem.Name} is out of stock");
            }
        }
    }
}
