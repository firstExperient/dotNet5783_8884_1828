using BlApi;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderProperty = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderWindow));

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public OrderWindow(int orderId)
        {
            Order = bl.Order.Get(orderId);
            InitializeComponent();
        }

        private void Remove_Item(object sender, RoutedEventArgs e)
        {
            //var element = e.OriginalSource as FrameworkElement;
            //if (element != null && element.DataContext is BO.OrderItem)
            //{
                
            //    new OrderWindow((element.DataContext as BO.OrderForList)!.ID).ShowDialog();
            //    OrdersList = bl?.Order.GetAll();
            //}
            //(.DataContext as BO.OrderItem).ID
        }
    }
}
