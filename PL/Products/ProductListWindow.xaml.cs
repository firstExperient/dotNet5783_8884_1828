
using BlApi;
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

    void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {

    }
        /*  private void OnComboBoxChanged(object sender, EventArgs e)
          {

          }*/
    }
