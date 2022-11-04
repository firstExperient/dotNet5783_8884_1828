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
                break;
            case 2:
                Console.WriteLine("Enter product id:");
                Int32.TryParse(Console.ReadLine(),out id);
                Product product = _dalProduct.Get(id);
                
                break;
        }
    }
    private static void TestingOrder() { }
    private static void TestingOrderItem() { }
}