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
        ProductsListview.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        // TODO: call get-products function from BL filtered by category and display

        
        string selection = ComboBox.SelectedItemProperty.ToString();
        MessageBox.Show(ComboBox.DataContextProperty.ToString());

        InitializeComponent();
        ProductsListview.ItemsSource = bl.Product.GetByCategory(selection.ToString());
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
    }
}
