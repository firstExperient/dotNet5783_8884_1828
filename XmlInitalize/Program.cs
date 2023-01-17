
using DO;
using System.Xml.Serialization;

namespace XmlInitalize;

internal class Program
{
    static Random Random = new Random();
    static void Main(string[] args)
    {
        List<DO.Order?> orders = new List<DO.Order?>();
        List<DO.OrderItem?> orderItems = new List<DO.OrderItem?>();
        List<DO.Product?> products = new List<DO.Product?>();

        // products:
        string[] watchNames = { "iceWatch", "Rolex", "DKNY", "Michael Kors", "Louis Vitton", "Tommy Hilfiger", "Casio", "Anna Klein", "Celvin Clein", "Q&Q" };


        for (int i = 0; i < 10; i++)
        {
            products.Add(new Product() { Name = watchNames[i], Category = (Category)Random.Next(0, 5), Price = Math.Round(Random.NextDouble() * 400, 1), InStock = i == 2 ? 0 : Random.Next(0, 350) });
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
            orders.Add(new Order()
            {
                ID = i,
                CustomerName = customerName[i % 10],
                CustomerEmail = email[i % 10],
                CustomerAdress = adress[i % 10],
                OrderDate = orderDate,
                ShipDate = ship < DateTime.Now ? ship : null,
                DeliveryDate = delivery < DateTime.Now ? delivery : null
            });
        }

        //orderItems:

        //the code will add an avarage of 2 items to an order wich will give about 40 order-items
        for (int i = 0; i < 20; i++)
        {
            int itemPerOrder = Random.Next(1, 5);//adding 1-4 items to an order
            for (int j = 0; j < itemPerOrder; j++)
            {
                int productIndex = Random.Next(0, products.Count);//selecting a random product to add
                orderItems.Add(new OrderItem()
                {
                    ProductId = (int)products[productIndex]?.ID!,
                    OrderId = (int)orders[i]?.ID!,
                    Price = (int)products[productIndex]?.Price!,
                    Amount = Random.Next(1, 5)
                });
            }
        }

        //save everything in xml:

        FilesManage<DO.Order?>.Save(orders, "Orders.xml");
        FilesManage<DO.Product?>.Save(products, "Products.xml");
        FilesManage<DO.OrderItem?>.Save(orderItems, "OrderItems.xml");

        IEnumerable<DO.Order?> ordersSaved = FilesManage<DO.Order?>.Read("Orders.xml");

        foreach (DO.Order? o in ordersSaved)
            Console.WriteLine(o);
    }

    public class FilesManage<T>
    {
        static public IEnumerable<T> Read(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));
            StreamReader sr = new StreamReader(@"../../../../xml/" + path);
            return xs.Deserialize(sr) as List<T> ?? throw new Exception("Fix this");
            sr.Close();
        }

        static public void Save(IEnumerable<T> list, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));
            FileStream fs = new FileStream(@"../../../../xml/" + path, FileMode.OpenOrCreate);
            xs.Serialize(fs, (List<T>)list);
            fs.Close();
        }
    }
}