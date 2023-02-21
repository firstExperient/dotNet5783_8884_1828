using BlApi;
using PL.Orders;
using PL.Products;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl? bl = BlApi.Factory.Get();
    
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
    private void TrackOrder(object sender, RoutedEventArgs e)
    {
        new OrderTrackingWindow().Show();
        Close();
    }

    private void NewOrder(object sender, RoutedEventArgs e)
    {
        new CatalogWindow().Show();
        Close();
    }
    private void Administration(object sender, RoutedEventArgs e)
    {
        new Administration().Show();
        Close();
    }

    private void StartSimulation(object sender, RoutedEventArgs e)
    {
       // new OrderTrackingWindow().Show();
        Close();
    }
}
