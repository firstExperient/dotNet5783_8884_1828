using BO;
using PL.Orders;
using System.Collections.ObjectModel;
using System.Windows;
using BlApi;
using PL.Products;

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty ListProperty
      = DependencyProperty.Register(nameof(ProductItem), typeof(ObservableCollection<BO.ProductForList?>), typeof(CatalogWindow));

        public ObservableCollection<BO.ProductForList?> ProductsList
        {
            get => (ObservableCollection<BO.ProductForList?>)GetValue(ListProperty);
            set => SetValue(ListProperty, value);
        }
        public CatalogWindow()
        {
            ProductsList = new ObservableCollection<BO.ProductForList?>(bl.Product.GetAll());
            InitializeComponent();
        }

        private void AddProductToCart(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element != null && element.DataContext is BO.OrderForList)
            {
                new ProductWindow((element.DataContext as BO.ProductForList)!.ID).ShowDialog();
                ProductsList = new ObservableCollection<BO.ProductForList?>(bl?.Product.GetAll()!);
            }
        }
    }
}
