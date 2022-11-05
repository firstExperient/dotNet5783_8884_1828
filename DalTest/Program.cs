using Dal;
using DO;
internal class Program
{
    private static void Main(string[] args)
    {
        int choice = MainMenu();
        while (choice != 0)
        {
            switch (choice)
            {
                case 0:
                    break;
                case 1: 
                    TestingProduct();
                    break;
                case 2:
                    TestingOrder();
                    break;
                case 3:
                    TestingOrderItem();
                    break;
                default:
                    Console.WriteLine("\nError! worng number input");//improve sentance if possible
                    break;
            }
            choice = MainMenu();
        }
    }

    private static int MainMenu()
    {
        Console.WriteLine("\nMAIN MENU\n");
        Console.WriteLine(" - Enter 0 to exit");
        Console.WriteLine(" - Enter 1 to test Product");
        Console.WriteLine(" - Enter 2 to test Order");
        Console.WriteLine(" - Enter 3 to test Order Item");
        int choice;
        bool success = Int32.TryParse(Console.ReadLine(),out choice);
        if(!success)
        {
            Console.WriteLine("\nError! input must be a number");
            return MainMenu();
        }
        return choice;
    }

    private static DalProduct _dalProduct = new DalProduct();

    private static DalOrder _dalOrder = new DalOrder();

    private static DalOrderItem _dalOrderItem = new DalOrderItem();
    private static void TestingProduct() {
        int choice = 0,id;
        Product product = new Product();
        Console.WriteLine(" - a. Enter 1 to add a product");
        Console.WriteLine(" - b. Enter 2 to get a product by id");
        Console.WriteLine(" - c. Enter 3 to get all products");
        Console.WriteLine(" - d. Enter 4 to update a product");
        Console.WriteLine(" - e. Enter 5 to delete a product");
        bool success = Int32.TryParse(Console.ReadLine(),out choice);
        if (!success)
        {
            Console.WriteLine("Error! input must be a number");
            TestingProduct();
            return;
        }
        switch (choice)
        {
            case 1:
                product = ReadProductData();
                try
                {
                    _dalProduct.Add(product);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                break;
            case 2:
                Console.WriteLine("Enter product id:");
                Int32.TryParse(Console.ReadLine(),out id);
                try
                {
                    product = _dalProduct.Get(id);
                    Console.Write(product);    
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                
                break;
            case 3:
                Product[] products = _dalProduct.GetAll();
                foreach (Product item in products)
                {
                    Console.Write(item);
                }
                break;
            case 4:
                Console.WriteLine("Enter product id:");
                Int32.TryParse(Console.ReadLine(), out id);
                product = ReadProductData();
                product.ID = id;
                try
                {
                    _dalProduct.Update(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 5:
                Console.WriteLine("Enter product id:");
                Int32.TryParse(Console.ReadLine(), out id);
                try
                {
                    _dalProduct.Delete(id);

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                break;
            default:
                Console.WriteLine("\nError! input is not valid");
                break;
        }
    }
    private static void TestingOrder()
    {
        int choice = 0, id;
        Order order = new Order();
        Console.WriteLine(" - a. Enter 1 to add an order");
        Console.WriteLine(" - b. Enter 2 to get an order by id");
        Console.WriteLine(" - c. Enter 3 to get all orders");
        Console.WriteLine(" - d. Enter 4 to update an order");
        Console.WriteLine(" - e. Enter 5 to delete an order");
        bool success = Int32.TryParse(Console.ReadLine(), out choice);
        if (!success)
        {
            Console.WriteLine("Error! input must be a number");
            TestingOrder();
            return;
        }
        switch (choice)
        {
            case 1:
                order = ReadOrderData();
                try
                {
                    _dalOrder.Add(order);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                break;
            case 2:
                Console.WriteLine("Enter order id:");
                Int32.TryParse(Console.ReadLine(), out id);
                try
                {
                    order = _dalOrder.Get(id);
                    Console.Write(order);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

                break;
            case 3:
                Order[] orders = _dalOrder.GetAll();
                foreach (Order item in orders)
                {
                    Console.Write(item);
                }
                break;
            case 4:
                Console.WriteLine("Enter order id:");
                Int32.TryParse(Console.ReadLine(), out id);
                order = ReadOrderData();
                order.ID = id;
                try
                {
                    _dalOrder.Update(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 5:
                Console.WriteLine("Enter order id:");
                Int32.TryParse(Console.ReadLine(), out id);
                try
                {
                    _dalOrder.Delete(id);

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                break;
            default:
                Console.WriteLine("\nError! input is not valid");
                break;
        }
    }
    private static void TestingOrderItem() { }

    private static Product ReadProductData()
    {
        int inStock,category;
        double price;
        string name;
        Console.WriteLine("Enter product name:");
        name = Console.ReadLine();
        Console.WriteLine("Enter th product category (0 - men watch, 1 - women watch, 2 - children watch, 3 - smart watch, 4 - diving watch");
        Int32.TryParse(Console.ReadLine(), out category);
        Console.WriteLine("Enter product price:");
        double.TryParse(Console.ReadLine(), out price);
        Console.WriteLine("Enter the amount of product in stock:");
        Int32.TryParse(Console.ReadLine(), out inStock);

        Product product = new Product() { Name = name, Price = price, InStock = inStock, Category = (Category)category };
        return product;
    }
    private static Order ReadOrderData()
    {
        string name, mail, adress;
        DateTime orderDate, ship, delivery;
        Console.WriteLine("Enter customer name:");
        name= Console.ReadLine();
        Console.WriteLine("Enter customer mail:");
        mail= Console.ReadLine();
        Console.WriteLine("Enter customer adress:");
        adress= Console.ReadLine();
        Console.WriteLine("Enter the date of the order:");
        DateTime.TryParse(Console.ReadLine(), out orderDate);
        Console.WriteLine("Enter the shipping date:");
        DateTime.TryParse(Console.ReadLine(), out ship);
        Console.WriteLine("Enter the delivery date:");
        DateTime.TryParse(Console.ReadLine(), out delivery);

        Order order = new Order()
        {
            CustomerName = name,
            CustomerEmail = mail,
            CustomerAdress = adress,
            OrderDate = orderDate,
            ShipDate = ship,
            DeliveryDate = delivery
        };
        return order;
    }
    private static OrderItem ReadItemData()
    {
        //maybe add a check the the product and the order ids do exist
        int productId, orderId,amount;
        double price;
        Console.WriteLine("Enter the order id:");
        Int32.TryParse(Console.ReadLine(), out orderId);
        Console.WriteLine("Enter the product id:");
        Int32.TryParse(Console.ReadLine(), out productId);
        Console.WriteLine("Enter the item price:");
        double.TryParse(Console.ReadLine(), out price);
        Console.WriteLine("Enter the amount:");
        Int32.TryParse(Console.ReadLine(), out amount);
        OrderItem orderItem = new OrderItem() { OrderId = orderId, ProductId = productId, Price = price, Amount = amount };
        return orderItem;
    }
}