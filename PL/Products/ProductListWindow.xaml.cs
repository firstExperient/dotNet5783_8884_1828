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
        MessageBox.Show("selection :" + ComboBox.SelectedItemProperty);
    }

    private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}
