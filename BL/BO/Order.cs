

using DO;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }  
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    //add status
    //public DateTime PaymentDate { get; set; } not sure that is needed
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

}
