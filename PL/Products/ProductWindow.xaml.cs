using BlApi;
using BlImplementation;
using System;
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
        ProductCategoryInput.ItemsSource = Enum.GetValues(typeof(BO.Category));

        //set the right look for the add mode
        ConfirmAddBtn.Visibility = Visibility.Visible;
        ConfirmUpdateBtn.Visibility = Visibility.Hidden;
    }

    public ProductWindow(int id)
    {
        InitializeComponent();
        ProductCategoryInput.ItemsSource = Enum.GetValues(typeof(BO.Category));
        BO.Product product = bl.Product.AdminGet(id);//fix this - add try and catch

        //initilaize the values of the inputs
        ProductIdInput.Text = product.ID.ToString();
        ProductNameInput.Text = product.Name;
        ProductPriceInput.Text = product.Price.ToString();
        ProductInStockInput.Text = product.InStock.ToString();
        ProductCategoryInput.SelectedItem = product.Category;

        //set the right look for the update mode
        ConfirmAddBtn.Visibility = Visibility.Hidden;
        ConfirmUpdateBtn.Visibility = Visibility.Visible;
        ProductIdInput.IsEnabled = false;
    }
    private void ConfirmAddBtn_Click(object sender, RoutedEventArgs e)
    {
        BO.Product product = new BO.Product()
        {
            ID = Convert.ToInt32(ProductIdInput.Text), //id is a vaild int, because of the input checking
            Name = ProductNameInput.Text,
            Category = (BO.Category)ProductCategoryInput.SelectedItem,
            Price = Convert.ToDouble(ProductPriceInput.Text),
            InStock = Convert.ToInt32(ProductInStockInput.Text),
        };
        bl.Product.Add(product);
        //fix this - how to update the list view?
        Close();
    }

    private void ConfirmUpdateBtn_Click(object sender, RoutedEventArgs e)
    {

        BO.Product product = new BO.Product()
        {
            ID = Convert.ToInt32(ProductIdInput.Text), 
            Name = ProductNameInput.Text,
            Category = (BO.Category)ProductCategoryInput.SelectedItem,
            Price = Convert.ToDouble(ProductPriceInput.Text),
            InStock = Convert.ToInt32(ProductInStockInput.Text),
        };
        bl.Product.Update(product);
        //fix this - how to update the list view?
        Close();
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
