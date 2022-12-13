using System.Reflection.Emit;
using System;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl = new Bl();
    

    public ProductWindow()
    {
        InitializeComponent();
    }

/*    private void ID_TextChanged(object sender, EventArgs e)
    {
 
    }*/


    private void addProduct_Click(object sender, RoutedEventArgs e)
    {
        // MessageBox.Show(id_input.Text);

        bl.Product.Add(new Product()
        {
            ID = Int32.Parse(id_input.Text),
            Name = name_input.Text,
            Price = Int32.Parse(price_input.Text),
            Category = "TODO: ",
            InStock = Int32.Parse(inStock_input.Text),
        });
    }
}
