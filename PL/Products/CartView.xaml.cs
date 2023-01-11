using BlApi;
using PL.Orders;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class CartView : Window
    {

        private IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(StateO), typeof(State), typeof(CartView));
        public State StateO
        {
            get => (State)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }
     /*   public static readonly DependencyProperty OrderProperty = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderWindow));
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }*/

        public CartView(/*int orderId, State state */)
        {
           // StateO = state;
          //  Order = bl.Order.Get(orderId);
            InitializeComponent();
        }
    }
}
