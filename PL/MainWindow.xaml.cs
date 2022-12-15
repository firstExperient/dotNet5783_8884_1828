using BlApi;
using PL.Products;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl = new BlImplementation.Bl();
    
    /// <summary>
    /// The main window of the program
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// This fuction opens the product list window
    /// </summary>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow().Show();
        Close();
    }
}
