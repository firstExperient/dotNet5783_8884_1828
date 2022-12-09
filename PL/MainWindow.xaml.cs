using BlApi;
using PL.Products;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private IBl bl = new BlImplementation.Bl();
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow().Show();
    }
}
