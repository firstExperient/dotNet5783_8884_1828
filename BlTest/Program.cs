using BlApi;
using BlImplementation;

namespace BlTest
{
    internal class Program
    {
        #region main

        /// <summary>
        /// this is the main program of the BlTest
        /// </summary>
        private static void Main(string[] args)
        {
            int choice = MainMenu();
            while (choice != 0)
            {
                try
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
                catch (BO.NegativeNumberException e)
                {
                    Console.Write(e);
                }
                catch (BO.IntegrityDamageException e)
                {
                    Console.Write(e);
                }
                catch (BO.NullValueException e)
                {
                    Console.Write(e);
                }
                catch (BO.NotFoundException e)
                {
                    Console.Write(e);
                }
                catch (BO.AlreadyExistsException e)
                {
                    Console.Write(e);
                }
                catch (BO.OutOfStockException e)
                {
                    Console.Write(e);
                }
                //no general exception catch - to allow debuging 
            }
        }

        /// <summary>
        /// the main menu of the project
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

        static private BO.Cart _cart = new BO.Cart();

        #region testing product

        /// <summary>
        /// The product menu
        /// </summary>
        private static void TestingProduct()
        {
            int choice = 0, id;
            BO.Product product;
            Console.WriteLine("\n - Enter 0 to return to main menu");
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
                        _bl.Product.Add(ReadProductData());
                    break;
                case 2:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.Write(_bl.Product.AdminGet(id));
                    break;
                case 3:
                    foreach (var item in _bl.Product.GetAll())
                    {
                        Console.Write(item);
                    }
                    break;
                case 4:
                    _bl.Product.Update(ReadProductData());
                    break;
                case 5:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                     _bl.Product.Delete(id);
                    break;
                case 6:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.Write(_bl.Product.Get(id,_cart));
                    break;
                default:
                    Console.WriteLine("\nError! input is not valid");
                    break;
            }
        }

        /// <summary>
        /// with this function the user can enter a product's data
        /// </summary>
        /// <returns>the product with the data the user entered</returns>
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
            int choice = 0, orderId;
            DateTime date;
            BO.Order order = new BO.Order();
            Console.WriteLine("\n - Enter 0 to return to main menu");
            Console.WriteLine(" - a. Enter 1 to track an order");
            Console.WriteLine(" - b. Enter 2 to get an order by id");
            Console.WriteLine(" - c. Enter 3 to get all orders");
            Console.WriteLine(" - d. Enter 4 to update an order");
            Console.WriteLine(" - e. Enter 5 to update order shipping");
            Console.WriteLine(" - f. Enter 6 to update order delivery");
            bool success = Int32.TryParse(Console.ReadLine(), out choice);
            if (!success)
            {
                Console.WriteLine("Error! input must be a number");
                TestingOrder();
                return;
            }
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out orderId);
                    Console.Write(_bl.Order.TrackOrder(orderId));
                    break;
                case 2:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out orderId);
                    Console.Write(_bl.Order.Get(orderId));
                    break;
                case 3:
                    foreach (BO.OrderForList item in _bl.Order.GetAll())
                    {
                        Console.Write(item);
                    }
                    break;
                case 4:
                    _bl.Order.UpdateOrder(ReadOrderData());
                    break;
                case 5:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out orderId);
                    Console.WriteLine("Enter order shipping date:");
                    DateTime.TryParse(Console.ReadLine(), out date);
                    _bl.Order.ShipOrder(orderId,date);
                    break;
                case 6:
                    Console.WriteLine("Enter order id:");
                    Int32.TryParse(Console.ReadLine(), out orderId);
                    Console.WriteLine("Enter order delivery date:");
                    DateTime.TryParse(Console.ReadLine(), out date);
                    _bl.Order.DeliverOrder(orderId, date);
                    break;
                default:
                    Console.WriteLine("\nError! input is not valid");
                    break;
            }
        }

        /// <summary>
        /// read the updated order data
        /// </summary>
        /// <returns>the order with the data the user entered</returns>
        private static BO.Order ReadOrderData()
        {
            
            List<BO.OrderItem> items = new List<BO.OrderItem>();
            int id, choice, amount, productId;
            Console.WriteLine("Enter order id:");
            Int32.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter 1 to enter new item data, Enter 0 to finish");
            Int32.TryParse(Console.ReadLine(), out choice);
            while(choice != 0)
            {
                Console.WriteLine("Enter the product id:");
                Int32.TryParse(Console.ReadLine(), out productId);
                Console.WriteLine("Enter the new amount:");
                Int32.TryParse(Console.ReadLine(), out amount);
                items.Add(new BO.OrderItem() { ProductId = productId, Amount = amount });
                Console.WriteLine("Enter 1 to enter new item data, Enter 0 to finish");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            
            BO.Order order = new BO.Order()
            {
                ID = id,
                Items = items
            };
            return order;
        }
        #endregion

        #region testing cart

        /// <summary>
        /// the cart menu
        /// </summary>
        private static void TestingCart()
        {
            int choice = 0, productId,amount;
            
            Console.WriteLine("\n - Enter 0 to return to main menu");
            Console.WriteLine(" - a. Enter 1 to add an item to cart");
            Console.WriteLine(" - b. Enter 2 to update an item amount in cart");
            Console.WriteLine(" - c. Enter 3 to confirm order");;

            bool success = Int32.TryParse(Console.ReadLine(), out choice);
            if (!success)
            {
                Console.WriteLine("Error! input must be a number");
                TestingCart();
                return;
            }
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out productId);
                    _bl.Cart.AddItem(productId, _cart);
                    break;
                case 2:
                    Console.WriteLine("Enter product id:");
                    Int32.TryParse(Console.ReadLine(), out productId);
                    Console.WriteLine("Enter the new amount:");
                    Int32.TryParse(Console.ReadLine(), out amount);
                    _bl.Cart.UpdateItemAmount(productId, _cart,amount);
                    break;
                case 3:
                    Console.WriteLine("Enter customer name:");
                    _cart.CustomerName = Console.ReadLine();
                    Console.WriteLine("Enter customer email:");
                    _cart.CustomerEmail = Console.ReadLine();
                    Console.WriteLine("Enter customer adress:");
                    _cart.CustomerAdress = Console.ReadLine();
                    _bl.Cart.ConfirmOrder(_cart);
                    _cart = new BO.Cart();
                    break;
                default:
                    Console.WriteLine("\nError! input is not valid");
                    break;
            }
        }
        #endregion
    }
}