using BlApi;
using BlImplementation;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(State), typeof(ProductWindow));
    public State State
    {
        get => (State)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public static readonly DependencyProperty ProductProperty = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(ProductWindow));
    public BO.Product Product {
        get => (BO.Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
        }

    public static IEnumerable Categories = Enum.GetValues(typeof(BO.Category));
    /// <summary>
    /// This is the window to add a new product
    /// </summary>
    public ProductWindow()
    {
        InitializeComponent();
        State = State.Add;
        Product = new();

        //set the right look for the add mode
        //ConfirmAddBtn.Visibility = Visibility.Visible;
        //ConfirmUpdateBtn.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// This is the window to edit a product (by its ID)
    /// </summary>
    public ProductWindow(int id)
    {
        InitializeComponent();
        State = State.Update;
        try
        {
            Product = bl.Product.AdminGet(id);
            //set the right look for the update mode
            //ConfirmAddBtn.Visibility = Visibility.Hidden;
            //ConfirmUpdateBtn.Visibility = Visibility.Visible;
            //ProductIdInput.IsEnabled = false;
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
        try
        {
            bl?.Product.Add(Product);
            Close();
        }
        catch (BO.AlreadyExistsException)
        {
            MessageBox.Show($"There is already a product with id: {Product.ID}, please choose a different ID for the product");
        }
    }

    /// <summary>
    /// This function updates the product with the new data the user entered
    /// </summary>
    private void ConfirmUpdateBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl?.Product.Update(Product);
        }
        catch (BO.NotFoundException)
        {
            MessageBox.Show($"No product with id: {Product.ID} was found in database.");
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
        if (e.Text == "." && (sender as TextBox).Text.IndexOf('.') == -1)
            e.Handled = false;
        else
        {
            Regex regex = new Regex(@"\D");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
    
}