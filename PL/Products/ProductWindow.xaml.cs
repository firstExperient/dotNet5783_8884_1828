using BlApi;
using BlImplementation;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl = new Bl();

    /// <summary>
    /// This is the window to add a new product
    /// </summary>
    public ProductWindow()
    {
        InitializeComponent();
        ProductCategoryInput.ItemsSource = Enum.GetValues(typeof(BO.Category));

        //set the right look for the add mode
        ConfirmAddBtn.Visibility = Visibility.Visible;
        ConfirmUpdateBtn.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// This is the window to edit a product (by its ID)
    /// </summary>
    public ProductWindow(int id)
    {
        InitializeComponent();
        ProductCategoryInput.ItemsSource = Enum.GetValues(typeof(BO.Category));
        try
        {
            BO.Product product = bl.Product.AdminGet(id);

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
        catch (BO.NegativeNumberException)
        {
            MessageBox.Show("The product ID seems to be a negative number, which causes errors");
            Close();
        }
        catch(BO.NotFoundException)
        {
            MessageBox.Show($"No product with id: {id} was found in database.");
            Close();
        }
    }

    /// <summary>
    /// This function adds a new product to the product list
    /// </summary>
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
        try
        {
            bl.Product.Add(product);
            Close();
        }
        catch (BO.AlreadyExistsException)
        {
            MessageBox.Show($"There is already a product with id: {product.ID}, please choose a different ID for the product");
        }
    }

    /// <summary>
    /// This function updates the product with the new data the user entered
    /// </summary>
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
        try
        {
            bl.Product.Update(product);
        }
        catch (BO.NotFoundException)
        {
            MessageBox.Show($"No product with id: {product.ID} was found in database.");
        }
        Close();
    }

    /// <summary>
    /// This function validates that the user entered an int type input
    /// </summary>
    private void IntIntputValidate(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = new Regex(@"\D");
        e.Handled = regex.IsMatch(e.Text);
    }

    /// <summary>
    /// This function validates that the user entered a double type input
    /// </summary>
    private void DoubleInputValidate(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        if (e.Text == "." && (sender as TextBox).Text.IndexOf('.') == -1)//fix this
            e.Handled = false;
        else
        {
            Regex regex = new Regex(@"\D");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}