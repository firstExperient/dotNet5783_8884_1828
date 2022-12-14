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

    /// <summary>
    /// This is the window which displays the products in a list form
    /// </summary>
    public ProductListWindow()
    {
        InitializeComponent();
        ProductsListview.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }


    /// <summary>
    /// This function displays products filtered by the chosen category
    /// </summary>
    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        ProductsListview.ItemsSource = bl.Product.GetByCategory((BO.Category)CategorySelector.SelectedItem);
    }

    /// <summary>
    /// This function opens a window to add a new product
    /// </summary>
    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();
        ProductsListview.ItemsSource = bl.Product.GetAll();
    }

    /// <summary>
    /// This function opens a window to edit the chosen product
    /// </summary>
    private void ProductsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        BO.ProductForList selectedProduct = (BO.ProductForList)ProductsListview.Items[ProductsListview.SelectedIndex];
        new ProductWindow(selectedProduct.ID).ShowDialog();
        ProductsListview.ItemsSource = bl.Product.GetAll();
    }

    /// <summary>
    /// A function to go back to previous window by the back button 
    /// </summary>
    private void backButton_Click(object sender, RoutedEventArgs e)
    {
       Close();
    }
}
