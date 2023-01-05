using BlApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ListProperty 
        = DependencyProperty.Register(nameof(ProductsList), typeof(IEnumerable<BO.ProductForList?>), typeof(ProductListWindow));

    public IEnumerable<BO.ProductForList?>? ProductsList
    {
        get => (IEnumerable<BO.ProductForList?>)GetValue(ListProperty);
        set => SetValue(ListProperty, value);
    }

    public static IEnumerable Categories = Enum.GetValues(typeof(BO.Category));

   
    
    /// <summary>
    /// This is the window which displays the products in a list form
    /// </summary>
    public ProductListWindow()
    {
        InitializeComponent();
        ProductsList = bl.Product.GetAll();
        //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }


    /// <summary>
    /// This function displays products filtered by the chosen category
    /// </summary>
    void CategorySelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        var element = args.OriginalSource as ComboBox;
        if (element != null)
        {
            ProductsList = bl?.Product.GetByCategory((BO.Category)element.SelectedItem);
        }
    }

    /// <summary>
    /// This function opens a window to add a new product
    /// </summary>
    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();
        ProductsList = bl?.Product.GetAll();
    }

    /// <summary>
    /// This function opens a window to edit the chosen product
    /// </summary>
    private void ProductsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //var element = ((e.OriginalSource as FrameworkElement).DataContext as BO.ProductForList).ID;
        //var name = element?.Name;
        //BO.ProductForList selectedProduct = (BO.ProductForList).Items[ProductsListview.SelectedIndex];
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.ProductForList)
        {
            new ProductWindow((element.DataContext as BO.ProductForList)!.ID).ShowDialog();
            ProductsList = bl?.Product.GetAll();
        }
        
    }

    /// <summary>
    /// A function to go back to previous window by the back button 
    /// </summary>
    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}
