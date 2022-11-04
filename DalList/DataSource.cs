using DO;

namespace Dal;

public static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }

    #region entities' array

    /// <summary>
    /// an array of the watches
    /// </summary>
    internal static Product[] Products = new Product[50];

    /// <summary>
    /// this function adds a new watch to config 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="category"></param>
    /// <param name="price"></param>
    /// <param name="inStock"></param>
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

    /// <summary>
    /// an array of the orders
    /// </summary>
    internal static Order[] Orders = new Order[100];

    /// <summary>
    /// this function adds a new order to config 
    /// </summary>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAdress"></param>
    /// <param name="orderDate"></param>
    /// <param name="shipDate"></param>
    /// <param name="deliveryDate"></param>
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

    /// <summary>
    /// an array of the order-items
    /// </summary>
    internal static OrderItem[] OrderItems = new OrderItem[200];

    /// <summary>
    ///  this function adds a new order-item to config 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="orderId"></param>
    /// <param name="price"></param>
    /// <param name="amount"></param>
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

    #region config

    /// <summary>
    /// 
    /// </summary>
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

        // items:

        private static void s_Initialize() {
        string[] watchNames = {"iceWatch", "Rolex", "DKNY","Michael Kors","Louis Vitton","Tommy Hilfiger","Casio","Anna Klein","Celvin Clein","Q&Q" };


        // orders:
        string[] customerName = { "Joey Fabian", "Recceca Levi", "Jossef Cohen", "Sarah Mendel", "Rachel Green", "Steeve McGarret", "Danny Williams", "Lizzie McGuaier", "Maddie Ziegler", "Zoey Brooks" };
        
        string[] email = { "JoeyFabian@gmail.com", "ReccecaLevi@gmail.com", "JossefCohen@gmail.com", "SarahMendel@gmail.com", "RachelGreen@gmail.com", "SteeveMcGarret@gmail.com", "DannyWilliams@gmail.com", "LizzieMcGuaier@gmail.com", "MaddieZiegler@gmail.com", "ZoeyBrooks@gmail.com" };
        
        string[] adress = { "Kazan 10 Ra'anana", "Mordechai Buxboim 12 Jerusalem", "Rabbi Akiva 34 Bnei-Brak", "Kakal 19 Tel-Aviv", "Ha'Melachim 65 Modi'in", "Shwarts 192 Kiriat-Malachi", "Ha'Shunit 1 Ashdod", "Sokolov 27 Holon", "Etrog 70 Herzelia", "Hakablan 18 Jerusalem" };

        for (int i = 0; i < 10; i++) {
            AddProduct(watchNames[i],(Category)Random.Next(0,10), Random.Next(100,1000),Random.Next(0,350));
        }
    }

    #endregion
}
