using PL.Orders;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }

        private void ShowOrders(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().Show();
        }
    }
}
