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
    private IBl bl = new Bl();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductsListview.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        string selection = ComboBox.SelectedItemProperty.ToString();

        // TODO: call get-products function from BL filtered by category

        MessageBox.Show("selection :" + selection);
    }
    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        //new ProductListWindow().Show();
    }
}
