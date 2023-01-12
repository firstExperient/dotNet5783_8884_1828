
using System.ComponentModel;
using System.Windows;

namespace PL;
/// <summary>
/// Interaction logic for SuccessOrderWindow.xaml
/// </summary>
public partial class SuccessOrderWindow : Window,INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    private int orderId;
    public int OrderId
    {
        get { return orderId; }
        set
        {
            orderId = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("OrderId"));
        }
    }

    public SuccessOrderWindow(int orderId)
    {
       
        OrderId = orderId;
        InitializeComponent();
    }

    private void ShowMain(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}
