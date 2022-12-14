using BlApi;
using BlImplementation;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new Bl();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductsGrid.ItemsSource = bl.Product.GetAll();
        //  ProductsListview.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        ProductsListview.ItemsSource = bl.Product.GetByCategory((BO.Category)CategorySelector.SelectedItem);
    }

    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
    }

    private void ProductsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        BO.ProductForList selectedProduct = (BO.ProductForList)ProductsListview.Items[ProductsListview.SelectedIndex];
        new ProductWindow(selectedProduct.ID).Show();
    }
}
