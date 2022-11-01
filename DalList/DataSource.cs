using DO;

namespace Dal;

public static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }


    #region entities' array

    internal static Product[] Products = new Product[50];

    private static void AddProduct(string name, Category category,double price, int inStock) {
        int id = Random.Next(100000, 1000000);

        //add here a check to see that the random id doesnt already exist

        Products[Config.ProductsIndex] = new Product()
        {
            ID = id,
            Name = name,
            Category = category,
            Price = price,
            InStock = inStock
        };

        Config.ProductsIndex++;
    }

    internal static Order[] Orders = new Order[100];
    
    private static void AddOrder(string customerName,string customerEmail,string customerAdress,DateTime orderDate,DateTime shipDate,DateTime deliveryDate) {
        int id = Config.OrderId;

        Orders[Config.OrdersIndex] = new Order()
        {
            ID = id,
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            CustomerAdress = customerAdress,
            OrderDate = orderDate,
            ShipDate = shipDate,
            DeliveryDate = deliveryDate
        };

        Config.OrdersIndex++;
    }

    internal static OrderItem[] OrderItems = new OrderItem[200];
    private static void AddOrderItem(int productId,int orderId,double price,int amount) {
        int id = Config.OrderItemId;

        OrderItems[Config.OrderItemsIndex] = new OrderItem()
        {
            ID = id,
            ProductId = productId,
            OrderId = orderId,
            Price = price,
            Amount = amount
        };

        Config.OrderItemsIndex++; 
    }

    #endregion

    internal static readonly Random Random = new Random();

    #region config
        internal class Config
        {

            static internal int ProductsIndex = 0;
            static internal int OrdersIndex = 0;
            static internal int OrderItemsIndex = 0;

            static private int orderId = 0;//check if 0 is the begining
            static internal int OrderId
            {
                get
                {
                    orderId++;
                    return orderId;
                }
            }

            static private int orderItemId = 0;//check this too

            static internal int OrderItemId
            {
                get
                {
                    orderItemId++;
                    return orderItemId;
                }
            }
        }

    #endregion

    #region initialize
        private static void s_Initialize() { }

    #endregion
}
