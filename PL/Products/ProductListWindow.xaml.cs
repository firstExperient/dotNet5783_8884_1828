using BlApi;
using BlImplementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl;
    public ProductListWindow(IBl Bl)
    {
        InitializeComponent();
        bl = Bl;
        ProductsListview.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        MessageBox.Show("selection :");
    }


    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
