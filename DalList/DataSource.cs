using DalApi;
using DO;

namespace Dal;
internal static class DataSource
{
    /// <summary>
    /// the DataSource constructor initialized the data
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }

    /// <summary>
    /// this method is used to force the static onstructor to run
    /// </summary>
    public static void Debug() { }

    /// <summary>
    /// a Random object to create random numbers
    /// </summary>
    internal static Random Random = new Random();

    #region entities' array

    /// <summary>
    /// a list of the watches
    /// </summary>
    internal static List<Product?> Products = new List<Product?>();

    /// <summary>
    /// this function adds a new watch
    /// </summary>
    /// <param name="name">name of the watch</param>
    /// <param name="category">category of the watch</param>
    /// <param name="price">price of the watch</param>
    /// <param name="inStock">how many watches are in stock</param>
    private static void AddProduct(string name, Category category, double price, int inStock)
    {
        int id = Random.Next(100000, 1000000);

        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i].HasValue && Products[i]!.Value.ID == id)
            {
                id = Random.Next(100000, 1000000);
                i = 0;
            }
        }
        Products.Add(new Product()
        {
            ID = id,
            Name = name,
            Category = category,
            Price = price,
            InStock = inStock
        });
    }

    /// <summary>
    /// a list of the orders
    /// </summary>

    internal static List<Order?> Orders = new List<Order?>();

    /// <summary>
    /// this function adds a new order
    /// </summary>
    /// <param name="customerName">customer's name</param>
    /// <param name="customerEmail">customer's email</param>
    /// <param name="customerAdress">customer's address</param>
    /// <param name="orderDate">the order date</param>
    /// <param name="shipDate">the shipment date</param>
    /// <param name="deliveryDate">the deliveration date</param>
    private static void AddOrder(string customerName, string customerEmail, string customerAdress, DateTime? orderDate, DateTime? shipDate, DateTime? deliveryDate)
    {
        int id = Config.OrderId;
        Orders.Add(new Order()
        {
            ID = id,
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            CustomerAdress = customerAdress,
            OrderDate = orderDate,
            ShipDate = shipDate,
            DeliveryDate = deliveryDate
        });
    }

    /// <summary>
    /// a list of the order-items
    /// </summary>
    internal static List<OrderItem?> OrderItems = new List<OrderItem?>();

    /// <summary>
    ///  this function adds a new order-item
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="orderId"></param>
    /// <param name="price"></param>
    /// <param name="amount"></param>
    private static void AddOrderItem(int productId, int orderId, double price, int amount)
    {
        int id = Config.OrderItemId;
        OrderItems.Add(new OrderItem()
        {
            ID = id,
            ProductId = productId,
            OrderId = orderId,
            Price = price,
            Amount = amount
        });
    }

    #endregion

    #region config

    /// <summary>
    /// holds all the configration information for the DataSource
    /// </summary>
    internal static class Config
    {

        static private int orderId = 0; //check if 0 is the begining
        static internal int OrderId
        {
            get
            {
                orderId++;
                return orderId;
            }
        }

        static private int orderItemId = 0; //check this too
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


    /// <summary>
    /// initialize the dataSource by adding random data to all the data's array
    /// </summary>
    private static void s_Initialize()
    {
        // items:
        string[] watchNames = { "iceWatch", "Rolex", "DKNY", "Michael Kors", "Louis Vitton", "Tommy Hilfiger", "Casio", "Anna Klein", "Celvin Clein", "Q&Q" };


        for (int i = 0; i < 10; i++)
        {
            AddProduct(watchNames[i], (Category)Random.Next(0, 5), Math.Round(Random.NextDouble() * 400, 1), i == 2 ? 0 : Random.Next(0, 350));
        }

        // orders:
        string[] customerName = { "Moshe Gross", "Rebecca Levi", "Jossef Cohen", "Sarah Mendel", "Rachel Greenfeld", "Haim Goldstein", "Esther Buxboim", "Arieh Zilbernagel", "Yechezkel Burstein", "Tzipora Chazan" };

        string[] email = { "MosheGross@gmail.com", "RebeccaLevi@gmail.com", "JossefCohen@gmail.com", "SarahMendel@gmail.com", "RachelGreenfeld@gmail.com", "HaimGoldstein@gmail.com", "EstherBuxboim@gmail.com", "AriehZilbernagel@gmail.com", "YechezkelBurstein@gmail.com", "TziporaChazan@gmail.com" };

        string[] adress = { "Kazan 10 Ra'anana", "Mordechai Buxboim 12 Jerusalem", "Rabbi Akiva 34 Bnei-Brak", "Kakal 19 Tel-Aviv", "Ha'Melachim 65 Modi'in", "Shwarts 192 Kiriat-Malachi", "Ha'Shunit 1 Ashdod", "Sokolov 27 Holon", "Etrog 70 Herzelia", "Hakablan 18 Jerusalem" };
        for (int i = 0; i < 20; i++)
        {
            DateTime orderDate = (DateTime.Now).Add(new TimeSpan(-Random.Next(24, 400), Random.Next(0, 60), Random.Next(0, 60)));
            DateTime ship = orderDate.Add(new TimeSpan(Random.Next(12, 96), Random.Next(0, 60), Random.Next(0, 60)));
            DateTime delivery = ship.Add(new TimeSpan(Random.Next(12, 96), Random.Next(0, 60), Random.Next(0, 60)));
            AddOrder(customerName[i % 10], email[i % 10], adress[i % 10], orderDate, ship < DateTime.Now ? ship : null, delivery < DateTime.Now ? delivery : null);
        }

        //orderItems:

        //the code will add an avarage of 2 items to an order wich will give about 40 order-items
        for (int i = 0; i < 20; i++)
        {
            int itemPerOrder = Random.Next(1, 5);//adding 1-4 items to an order
            for (int j = 0; j < itemPerOrder; j++)
            {
                int productIndex = Random.Next(0, Products.Count);//selecting a random product to add
                AddOrderItem(Products[productIndex]!.Value.ID, Orders[i]!.Value.ID, Products[productIndex]!.Value.Price, Random.Next(1, 5));
            }
        }
    }

    #endregion
}