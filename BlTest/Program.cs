using BlApi;
using BlImplementation;
using System.Linq.Expressions;

namespace BlTest
{
    internal class Program
    {
        #region main
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
                        TestingCart();
                        break;
                    default:
                        Console.WriteLine("\nError! worng number input");
                        break;
                }
                choice = MainMenu();
            }
        }

        /// <summary>
        /// the user must choose an option in the main menu
        /// </summary>
        private static int MainMenu()
        {
            Console.WriteLine("\nMAIN MENU\n");
            Console.WriteLine(" - Enter 0 to exit");
            Console.WriteLine(" - Enter 1 to test Product");
            Console.WriteLine(" - Enter 2 to test Order");
            Console.WriteLine(" - Enter 3 to test Cart");
            int choice;
            bool success = Int32.TryParse(Console.ReadLine(), out choice);
            if (!success)
            {
                Console.WriteLine("\nError! input must be a number");
                return MainMenu();
            }
            return choice;
        }
        #endregion

        static private IBl _bl = new Bl();

        #region testing product
        /// <summary>
        /// The product menu
        /// </summary>
        private static void TestingProduct()
        {
            int choice = 0, id;
            BO.Product product;
            Console.WriteLine(" - Enter 0 to return to main menu");
            Console.WriteLine(" - a. Enter 1 to add a product");
            Console.WriteLine(" - b. Enter 2 to get a product by id (administrator view)");
            Console.WriteLine(" - c. Enter 3 to get all products");
            Console.WriteLine(" - d. Enter 4 to update a product");
            Console.WriteLine(" - e. Enter 5 to delete a product");
            Console.WriteLine(" - f. Enter 6 to get a product for the catalog (costumer view)");

            bool success = Int32.TryParse(Console.ReadLine(), out choice);

            if (!success)
            {
                Console.WriteLine("Error! input must be a number");
                TestingProduct();
                return;
            }

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    try
                    {
                        _bl.Product.Add(ReadProductData());
                    }
                    catch (Exception)
                    {
                        //fix this
                        throw;
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        product = _bl.Product.AdminGet(id);
                        Console.Write(product);
                    }
                    catch (Exception e)
                    {
                        //fix this
                        Console.Write(e.Message);
                    }

                    break;
                case 3:
                    IEnumerable<BO.ProductForList> products = _bl.Product.GetAll();
                    foreach (var item in products)
                    {
                        Console.Write(item);
                    }
                    break;
                case 4:
                    product = ReadProductData();
                    try
                    {
                        _bl.Product.Update(product);
                    }
                    catch (Exception e)
                    {
                        //fix this
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        _bl.Product.Delete(id);
                    }
                    catch (Exception e)
                    {
                        //fix this
                        Console.Write(e.Message);
                    }
                    break;
                default:
                    Console.WriteLine("\nError! input is not valid");
                    break;
            }
        }
        private static BO.Product ReadProductData()
        {
            int inStock, category,id;
            double price;
            string name;
            Console.WriteLine("Enter the product ID:");
            Int32.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter product name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter the product category (0 - men watch, 1 - women watch, 2 - children watch, 3 - smart watch, 4 - diving watch");
            Int32.TryParse(Console.ReadLine(), out category);
            Console.WriteLine("Enter product price:");
            double.TryParse(Console.ReadLine(), out price);
            Console.WriteLine("Enter the amount of product in stock:");
            Int32.TryParse(Console.ReadLine(), out inStock);

            BO.Product product = new BO.Product() {ID=id, Name = name, Price = price, InStock = inStock, Category = (BO.Category)category };
            return product;
        }
        #endregion

        #region testing order

        /// <summary>
        /// The order menu
        /// </summary>
        private static void TestingOrder()
        {
            int choice = 0, id;
            BO.Order order = new BO.Order();
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
                    _dalList.Order.Add(ReadOrderData());
                    break;
                case 2:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        order = _dalList.Order.Get(id);
                        Console.Write(order);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }

                    break;
                case 3:
                    IEnumerable<Order> orders = _dalList.Order.GetAll();
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
                        _dalList.Order.Update(order);
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
                        _dalList.Order.Delete(id);

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
        private static Order ReadOrderData()
        {
            string name, mail, adress;
            DateTime orderDate, ship, delivery;
            Console.WriteLine("Enter customer name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter customer mail:");
            mail = Console.ReadLine();
            Console.WriteLine("Enter customer adress:");
            adress = Console.ReadLine();
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
        #endregion

        #region testing cart

        
        private static void TestingCart()
        {
            int choice = 0, id, productId;
            OrderItem orderItem = new OrderItem();
            IEnumerable<OrderItem> items;

            Console.WriteLine(" - a. Enter 1 to add an order item");
            Console.WriteLine(" - b. Enter 2 to get an order item by id");
            Console.WriteLine(" - c. Enter 3 to get all orders' items");
            Console.WriteLine(" - d. Enter 4 to get an order item by order and product ids");
            Console.WriteLine(" - e. Enter 5 to get all order items by order id");
            Console.WriteLine(" - d. Enter 6 to update an order item");
            Console.WriteLine(" - e. Enter 7 to delete an order item");

            bool success = Int32.TryParse(Console.ReadLine(), out choice);
            if (!success)
            {
                Console.WriteLine("Error! input must be a number");
                TestingOrderItem();
                return;
            }
            switch (choice)
            {
                case 1:
                    _dalList.OrderItem.Add(ReadItemData());
                    break;
                case 2:
                    Console.WriteLine("Enter order item id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        orderItem = _dalList.OrderItem.Get(id);
                        Console.Write(orderItem);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }

                    break;
                case 3:
                    items = _dalList.OrderItem.GetAll();
                    foreach (OrderItem item in items)
                    {
                        Console.Write(item);
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out productId);
                    try
                    {
                        orderItem = _dalList.OrderItem.GetItemByIds(id, productId);
                        Console.Write(orderItem);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    items = _dalList.OrderItem.GetAllItemsInOrder(id);
                    foreach (OrderItem item in items)
                    {
                        Console.Write(item);
                    }
                    break;
                case 6:
                    Console.WriteLine("Enter order item id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    orderItem = ReadItemData();
                    orderItem.ID = id;
                    try
                    {
                        _dalList.OrderItem.Update(orderItem);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 7:
                    Console.WriteLine("Enter order item id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        _dalList.OrderItem.Delete(id);
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
        /// <summary>
        /// function to create a new item
        /// </summary>
        /// <returns>the new order-item created by user</returns>
        private static OrderItem ReadItemData()
        {
            //maybe add a check if the product and the order's ids exist
            int productId, orderId, amount;
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
        #endregion

    }
}